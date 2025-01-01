using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxLayoutGroup : UIBehaviour, IUniFlexBoxLayoutElement, ILayoutGroup
    {
        [SerializeField]
        private FlexDirection _flexDirection;

        public FlexDirection FlexDirection
        {
            get => _flexDirection;
            set => SetProperty(ref _flexDirection, value);
        }

        [SerializeField]
        private AlignItems _alignItems;

        public AlignItems AlignItems
        {
            get => _alignItems;
            set => SetProperty(ref _alignItems, value);
        }

        [SerializeField]
        private JustifyContent _justifyContent;

        public JustifyContent JustifyContent
        {
            get => _justifyContent;
            set => SetProperty(ref _justifyContent, value);
        }

        [SerializeField]
        private DimensionProperties _minimumWidth;

        public DimensionProperties MinimumWidth
        {
            get => _minimumWidth;
            set => SetProperty(ref _minimumWidth, value);
        }

        [SerializeField]
        private DimensionProperties _exactWidth;

        public DimensionProperties ExactWidth
        {
            get => _exactWidth;
            set => SetProperty(ref _exactWidth, value);
        }

        [SerializeField]
        private DimensionProperties _maximumWidth;

        public DimensionProperties MaximumWidth
        {
            get => _maximumWidth;
            set => SetProperty(ref _maximumWidth, value);
        }

        [SerializeField]
        private DimensionProperties _minimumHeight;

        public DimensionProperties MinimumHeight
        {
            get => _minimumHeight;
            set => SetProperty(ref _minimumHeight, value);
        }

        [SerializeField]
        private DimensionProperties _exactHeight;

        public DimensionProperties ExactHeight
        {
            get => _exactHeight;
            set => SetProperty(ref _exactHeight, value);
        }

        [SerializeField]
        private DimensionProperties _maximumHeight;

        public DimensionProperties MaximumHeight
        {
            get => _maximumHeight;
            set => SetProperty(ref _maximumHeight, value);
        }

        [SerializeField]
        private AlignContent _alignContent;

        public AlignContent AlignContent
        {
            get => _alignContent;
            set => SetProperty(ref _alignContent, value);
        }

        [SerializeField]
        private RectOffset _padding;

        public RectOffset Padding
        {
            get => _padding;
            set => SetProperty(ref _padding, value);
        }

        public float minWidth => _minimumWidth.Size;
        public float preferredWidth { get; private set; }
        public float flexibleWidth { get; private set; }
        public float minHeight => _minimumHeight.Size;
        public float preferredHeight { get; private set; }
        public float flexibleHeight { get; private set; }

        public int layoutPriority { get; private set; }

        private readonly List<RectTransform> _rectChildren = new List<RectTransform>();

        [NonSerialized]
        private RectTransform _rect;

        private RectTransform RectTransform
        {
            get
            {
                if (_rect == null)
                {
                    _rect = GetComponent<RectTransform>();
                }

                return _rect;
            }
        }

        private DrivenRectTransformTracker _tracker;
        private IntPtr _rootYogaNode;
        private readonly Dictionary<RectTransform, IntPtr> _childNodes = new Dictionary<RectTransform, IntPtr>();

        public void CalculateLayoutInputHorizontal()
        {
            UpdateChildren();
            _tracker.Clear();
            if (_rootYogaNode == IntPtr.Zero)
            {
                _rootYogaNode = UniFlexBoxNative.createNewNode();
            }

            UniFlexBoxNative.setFlexDirection(_rootYogaNode, (int)_flexDirection);
            UniFlexBoxNative.setAlignItems(_rootYogaNode, (int)_alignItems);
            UniFlexBoxNative.setJustifyContent(_rootYogaNode, (int)_justifyContent);

            this.ApplySizeDimensions(_rootYogaNode, RectTransform);

            UniFlexBoxNative.removeAllChildren(_rootYogaNode);
            var activeNodes = new HashSet<IntPtr>();
            foreach (RectTransform child in _rectChildren)
            {
                if (!_childNodes.TryGetValue(child, out IntPtr childYogaNode))
                {
                    childYogaNode = UniFlexBoxNative.createNewNode();
                    _childNodes.Add(child, childYogaNode);
                }

                var layoutElement = child.GetComponent<IUniFlexBoxLayoutElement>();
                if (layoutElement != null)
                {
                    layoutElement.ApplySizeDimensions(childYogaNode, child);
                }
                else
                {
                    UniFlexBoxNative.setNodeWidth(childYogaNode, child.rect.width);
                    UniFlexBoxNative.setNodeHeight(childYogaNode, child.rect.height);
                }

                UniFlexBoxNative.addChild(_rootYogaNode, childYogaNode);
                activeNodes.Add(childYogaNode);
            }

            List<KeyValuePair<RectTransform, IntPtr>> unusedNodes =
                _childNodes.Where(entry => !activeNodes.Contains(entry.Value)).ToList();
            foreach (KeyValuePair<RectTransform, IntPtr> unusedNode in unusedNodes)
            {
                UniFlexBoxNative.freeNode(unusedNode.Value);
                _childNodes.Remove(unusedNode.Key);
            }

            UniFlexBoxNative.calculateLayout(_rootYogaNode);

            ApplyNodeValuesToLayoutGroup();
            foreach (RectTransform child in _rectChildren)
            {
                ApplyNodeValuesToLayoutElement(_childNodes[child], child);
            }
        }

        private void ApplyNodeValuesToLayoutGroup()
        {
            preferredWidth = UniFlexBoxNative.getNodeWidth(_rootYogaNode);
            preferredHeight = UniFlexBoxNative.getNodeHeight(_rootYogaNode);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredWidth);
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredHeight);
        }

        private void ApplyNodeValuesToLayoutElement(IntPtr yogaNode, RectTransform uiElement)
        {
            float x = UniFlexBoxNative.getNodeLeft(yogaNode);
            float y = UniFlexBoxNative.getNodeTop(yogaNode);
            float width = UniFlexBoxNative.getNodeWidth(yogaNode);
            float height = UniFlexBoxNative.getNodeHeight(yogaNode);

            uiElement.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, x, width);
            uiElement.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, y, height);
        }

        public void CalculateLayoutInputVertical() { }

        private void UpdateChildren()
        {
            _rectChildren.Clear();
            for (var i = 0; i < RectTransform.childCount; ++i)
            {
                var rect = RectTransform.GetChild(i) as RectTransform;
                if (rect == null || !rect.gameObject.activeInHierarchy)
                {
                    continue;
                }

                ILayoutIgnorer[] toIgnoreList = rect.GetComponents<ILayoutIgnorer>();

                if (toIgnoreList.Length == 0)
                {
                    _rectChildren.Add(rect);
                    continue;
                }

                if (toIgnoreList.Any(t => !t.ignoreLayout))
                {
                    _rectChildren.Add(rect);
                }
            }
        }

        public void SetLayoutHorizontal()
        {
        }

        public void SetLayoutVertical()
        {
        }

        /// <summary>
        /// Helper method used to set a given property if it has changed.
        /// </summary>
        /// <param name="currentValue">A reference to the member value.</param>
        /// <param name="newValue">The new value.</param>
        private void SetProperty<T>(ref T currentValue, T newValue)
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                return;
            currentValue = newValue;
            SetDirty();
        }

        /// <summary>
        /// Mark the LayoutGroup as dirty.
        /// </summary>
        private void SetDirty()
        {
            if (!IsActive())
                return;

            if (!CanvasUpdateRegistry.IsRebuildingLayout())
            {
                LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
            }
            else
            {
                StartCoroutine(DelayedSetDirty(RectTransform));
            }
        }

        IEnumerator DelayedSetDirty(RectTransform rectTransform)
        {
            yield return null;
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
        }
#endif
    }
}