using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxLayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup
    {
        [SerializeField]
        private FlexDirection _flexDirection;

        public FlexDirection FlexDirection
        {
            get { return _flexDirection; }
            set { SetProperty(ref _flexDirection, value); }
        }

        [SerializeField]
        private AlignItems _alignItems;

        public AlignItems AlignItems
        {
            get { return _alignItems; }
            set { SetProperty(ref _alignItems, value); }
        }

        [SerializeField]
        private JustifyContent _justifyContent;

        public JustifyContent JustifyContent
        {
            get { return _justifyContent; }
            set { SetProperty(ref _justifyContent, value); }
        }

        [SerializeField]
        private DimensionProperties _minimumWidth;

        public DimensionProperties MinimumWidth
        {
            get { return _minimumWidth; }
            set { SetProperty(ref _minimumWidth, value); }
        }

        [SerializeField]
        private DimensionProperties _maximumWidth;

        public DimensionProperties MaximumWidth
        {
            get { return _maximumWidth; }
            set { SetProperty(ref _maximumWidth, value); }
        }

        [SerializeField]
        private DimensionProperties _minimumHeight;

        public DimensionProperties MinimumHeight
        {
            get { return _minimumHeight; }
            set { SetProperty(ref _minimumHeight, value); }
        }

        [SerializeField]
        private DimensionProperties _maximumHeight;

        public DimensionProperties MaximumHeight
        {
            get { return _maximumHeight; }
            set { SetProperty(ref _maximumHeight, value); }
        }

        [SerializeField]
        private AlignContent _alignContent;

        public AlignContent AlignContent
        {
            get { return _alignContent; }
            set { SetProperty(ref _alignContent, value); }
        }

        [Header("Constraints")]
        [Tooltip("If defined, overrides the minimum and maximum width constraints.")]
        [SerializeField]
        private float _definiteWidth;

        public float DefiniteWidth
        {
            get { return _definiteWidth; }
            set { SetProperty(ref _definiteWidth, value); }
        }

        [SerializeField]
        private RectOffset _padding;

        public RectOffset Padding
        {
            get { return _padding; }
            set { SetProperty(ref _padding, value); }
        }

        [Tooltip("If defined, overrides the minimum and maximum height constraints.")]
        [SerializeField]
        private float _definiteHeight;

        public float DefiniteHeight
        {
            get { return _definiteHeight; }
            set { SetProperty(ref _definiteHeight, value); }
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

        private RectTransform _rectTransform
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
        private Dictionary<RectTransform, IntPtr> _childNodes = new Dictionary<RectTransform, IntPtr>();

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
            ApplyMinimumWidthDimensions(_rootYogaNode, _minimumWidth);
            ApplyMaximumWidthDimensions(_rootYogaNode, _maximumWidth);
            ApplyMinimumHeightDimensions(_rootYogaNode, _minimumHeight);
            ApplyMinimumHeightDimensions(_rootYogaNode, _maximumHeight);

            UniFlexBoxNative.removeAllChildren(_rootYogaNode);
            var activeNodes = new HashSet<IntPtr>();
            foreach (var child in _rectChildren)
            {
                if (!_childNodes.TryGetValue(child, out IntPtr childYogaNode))
                {
                    childYogaNode = UniFlexBoxNative.createNewNode();
                    UniFlexBoxNative.setNodeWidth(childYogaNode, 100f);
                    UniFlexBoxNative.setNodeHeight(childYogaNode, 100f);
                    _childNodes.Add(child, childYogaNode);
                }

                UniFlexBoxNative.addChild(_rootYogaNode, childYogaNode);
                activeNodes.Add(childYogaNode);
            }

            var unusedNodes = _childNodes.Where(entry => !activeNodes.Contains(entry.Value));
            foreach (var unusedNode in unusedNodes)
            {
                UniFlexBoxNative.freeNode(unusedNode.Value);
                _childNodes.Remove(unusedNode.Key);
            }

            UniFlexBoxNative.calculateLayout(_rootYogaNode);

            ApplyLayout(_rootYogaNode, _rectTransform, _rectTransform.parent as RectTransform);
            foreach (var child in _rectChildren)
            {
                ApplyLayout(_childNodes[child], child, _rectTransform);
            }

            return;
            float[] containerWidth =
                CalculateContainerSize(_definiteWidth, _minimumWidth.Size, ref _maximumWidth.Size, 0);
            float[] availableHorizontalSpace = CalculateAvailableSpace(containerWidth, 0);
            float[] containerHeight =
                CalculateContainerSize(_definiteHeight, _minimumHeight.Size, ref _maximumHeight.Size, 1);
            float[] availableVerticalSpace = CalculateAvailableSpace(containerHeight, 1);

            Debug.Log(
                $"UniFlexBox: Container size, horizontal: ({containerWidth[0]}, {containerWidth[1]}), vertical: ({containerHeight[0]}, {containerHeight[1]})");
            Debug.Log(
                $"UniFlexBox: Available space, horizontal: ({availableHorizontalSpace[0]}, {availableHorizontalSpace[1]}), vertical: ({availableVerticalSpace[0]}, {availableVerticalSpace[1]})");

            var baseSizes = new List<float[]>();
            var hypotheticalMainSizes = new List<float[]>();
            for (var i = 0; i < _rectChildren.Count; ++i)
            {
                var child = _rectChildren[i];
                var childRect = child.rect;
                var flexboxItem = child.GetComponent<UniFlexBoxItem>();
                if (flexboxItem == null)
                {
                    baseSizes.Add(new[] { childRect.size[0], childRect.size[1] });
                    continue;
                }

                float[] baseWidth;
                if (flexboxItem.WrapWidth)
                {
                    var layoutElements = child.GetComponentsInChildren<ILayoutElement>();
                    float layoutElementWidth =
                        layoutElements.Length > 0
                            ? layoutElements.Max(e => e.preferredWidth)
                            : childRect.size[0];
                    float maxChildSize = child.Cast<RectTransform>().Max(e => e.rect.size[0]);
                    var maxWidth = Mathf.Max(layoutElementWidth, maxChildSize);
                    baseWidth = new[] { maxWidth, maxWidth };
                }
                else
                {
                }
            }
        }

        private void ApplyMinimumWidthDimensions(IntPtr node, DimensionProperties minimumWidth)
        {
            if (!minimumWidth.Use)
            {
                return;
            }

            UniFlexBoxNative.setNodeMinWidth(_rootYogaNode, minimumWidth.Size);
        }

        private void ApplyMaximumWidthDimensions(IntPtr node, DimensionProperties maximumWidth)
        {
            if (!maximumWidth.Use)
            {
                return;
            }

            UniFlexBoxNative.setNodeMaxWidth(_rootYogaNode, maximumWidth.Size);
        }

        private void ApplyMinimumHeightDimensions(IntPtr node, DimensionProperties minimumHeight)
        {
            if (!minimumHeight.Use)
            {
                return;
            }

            UniFlexBoxNative.setNodeMinHeight(_rootYogaNode, minimumHeight.Size);
        }

        private void ApplyMaximumHeightDimensions(IntPtr node, DimensionProperties maximumHeight)
        {
            if (!maximumHeight.Use)
            {
                return;
            }

            UniFlexBoxNative.setNodeMaxHeight(_rootYogaNode, maximumHeight.Size);
        }

        public void ApplyLayout(IntPtr yogaNode, RectTransform uiElement, RectTransform parent)
        {
            // Retrieve layout properties from the YogaNode
            float x = UniFlexBoxNative.getNodeLeft(yogaNode);
            float y = UniFlexBoxNative.getNodeTop(yogaNode);
            float width = UniFlexBoxNative.getNodeWidth(yogaNode);
            float height = UniFlexBoxNative.getNodeHeight(yogaNode);

            // Get the parent size for positioning conversion
            float parentHeight = parent.rect.height;

            // Adjust RectTransform
            uiElement.anchorMin = Vector2.zero;
            uiElement.anchorMax = Vector2.zero;
            uiElement.pivot = Vector2.zero;
            uiElement.anchoredPosition =
                new Vector2(x, parentHeight - y - height); // Convert Yoga top-left to Unity bottom-left
            uiElement.sizeDelta = new Vector2(width, height); // Set width and height
        }

        public void CalculateLayoutInputVertical()
        {
        }

        private float[] CalculateContainerSize(float definiteSize, float minimumSize, ref float maximumSize, int axis)
        {
            if (definiteSize > 0)
            {
                return new[] { definiteSize, definiteSize };
            }
            else if (minimumSize > 0 || maximumSize > 0)
            {
                float finalMaximumSize;
                if (maximumSize > 0)
                {
                    if (maximumSize <= minimumSize)
                    {
                        maximumSize = minimumSize;
                    }

                    finalMaximumSize = maximumSize;
                }
                else
                {
                    finalMaximumSize = float.MaxValue;
                }

                return new[] { minimumSize, finalMaximumSize };
            }
            else
            {
                var size = _rectTransform.rect.size[axis];
                return new[] { size, size };
            }
        }

        private float[] CalculateAvailableSpace(float[] containerSize, int axis)
        {
            float padding = axis == 0 ? _padding.horizontal : _padding.vertical;
            float maxSize = containerSize[1] - padding;
            float minSize = containerSize[0];
            if (maxSize < 0)
            {
                return new[] { 0f, 0f };
            }
            else if (maxSize < minSize)
            {
                return new[] { maxSize, maxSize };
            }
            else
            {
                return new[] { minSize, maxSize };
            }
        }

        private void UpdateChildren()
        {
            _rectChildren.Clear();
            for (int i = 0; i < _rectTransform.childCount; ++i)
            {
                var rect = _rectTransform.GetChild(i) as RectTransform;
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

                for (int j = 0; j < toIgnoreList.Length; ++j)
                {
                    if (!toIgnoreList[j].ignoreLayout)
                    {
                        _rectChildren.Add(rect);
                        break;
                    }
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
                LayoutRebuilder.MarkLayoutForRebuild(_rectTransform);
            }
            else
            {
                StartCoroutine(DelayedSetDirty(_rectTransform));
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