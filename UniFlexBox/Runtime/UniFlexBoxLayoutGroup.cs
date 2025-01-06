using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxLayoutGroup : UIBehaviour, IUniFlexBoxLayoutGroup, ILayoutGroup
    {
        [SerializeField]
        private YGDirection _direction;

        public YGDirection Direction
        {
            get => _direction;
            set => SetProperty(ref _direction, value);
        }

        [SerializeField]
        private YGFlexDirection _flexDirection;

        public YGFlexDirection FlexDirection
        {
            get => _flexDirection;
            set => SetProperty(ref _flexDirection, value);
        }

        [SerializeField]
        private YGJustify _justifyContent;

        public YGJustify JustifyContent
        {
            get => _justifyContent;
            set => SetProperty(ref _justifyContent, value);
        }

        [SerializeField]
        private YGAlign _alignContent;

        public YGAlign AlignContent
        {
            get => _alignContent;
            set => SetProperty(ref _alignContent, value);
        }

        [SerializeField]
        private YGAlign _alignItems;

        public YGAlign AlignItems
        {
            get => _alignItems;
            set => SetProperty(ref _alignItems, value);
        }

        [SerializeField]
        private YGAlign _alignSelf;

        public YGAlign AlignSelf
        {
            get => _alignSelf;
            set => SetProperty(ref _alignSelf, value);
        }

        [SerializeField]
        private YGWrap _flexWrap;

        public YGWrap FlexWrap
        {
            get => _flexWrap;
            set => SetProperty(ref _flexWrap, value);
        }

        [SerializeField]
        private YGOverflow _overflow;

        public YGOverflow Overflow
        {
            get => _overflow;
            set => SetProperty(ref _overflow, value);
        }

        [SerializeField]
        private YGDisplay _display;

        public YGDisplay Display
        {
            get => _display;
            set => SetProperty(ref _display, value);
        }

        [SerializeField]
        private float _flex;

        public float Flex
        {
            get => _flex;
            set => SetProperty(ref _flex, value);
        }

        [SerializeField]
        private float _flexGrow;

        public float FlexGrow
        {
            get => _flexGrow;
            set => SetProperty(ref _flexGrow, value);
        }

        [SerializeField]
        private float _flexShrink;

        public float FlexShrink
        {
            get => _flexShrink;
            set => SetProperty(ref _flexShrink, value);
        }

        [SerializeField]
        private List<DimensionConstraint> _dimensionConstraints;

        public List<DimensionConstraint> DimensionConstraints
        {
            get => _dimensionConstraints;
            set => SetProperty(ref _dimensionConstraints, value);
        }

        [SerializeField]
        private List<PaddingConstraint> _paddingConstraints;

        public List<PaddingConstraint> PaddingConstraints
        {
            get => _paddingConstraints;
            set => SetProperty(ref _paddingConstraints, value);
        }

        [SerializeField]
        private List<GapConstraint> _gapConstraints;

        public List<GapConstraint> GapConstraints
        {
            get => _gapConstraints;
            set => SetProperty(ref _gapConstraints, value);
        }

        public float minWidth =>
            _dimensionConstraints?.Any(dc => dc.Type == ConstraintType.MinimumWidth) ?? false
                ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumWidth).Value
                : 0f;

        public float preferredWidth { get; private set; }
        public float flexibleWidth { get; private set; }

        public float minHeight =>
            _dimensionConstraints?.Any(dc => dc.Type == ConstraintType.MinimumHeight) ?? false
                ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumHeight).Value
                : 0f;

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

            UniFlexBoxNative.setNodeDirection(_rootYogaNode, (int)_direction);
            UniFlexBoxNative.setNodeFlexDirection(_rootYogaNode, (int)_flexDirection);
            UniFlexBoxNative.setNodeJustifyContent(_rootYogaNode, (int)_justifyContent);
            UniFlexBoxNative.setNodeAlignContent(_rootYogaNode, (int)_alignContent);
            UniFlexBoxNative.setNodeAlignItems(_rootYogaNode, (int)_alignItems);
            UniFlexBoxNative.setNodeFlexWrap(_rootYogaNode, (int)_flexWrap);

            this.ApplyLayoutElementToNode(_rootYogaNode);
            this.ApplyPaddingConstraintsToNode(_rootYogaNode);
            this.ApplyGapConstraintsToNode(_rootYogaNode);

            UniFlexBoxNative.removeAllChildren(_rootYogaNode);
            foreach (KeyValuePair<RectTransform, IntPtr> childNodePair in _childNodes)
            {
                UniFlexBoxNative.freeNode(childNodePair.Value);
            }

            _childNodes.Clear();

            foreach (RectTransform child in _rectChildren)
            {
                IntPtr childYogaNode = UniFlexBoxNative.createNewNode();

                ILayoutElement layoutElement = GetLayoutElementComponent(child);
                if (layoutElement is IUniFlexBoxLayoutElement ufbLayoutElement
                    && ufbLayoutElement.DimensionConstraints.Any())
                {
                    ufbLayoutElement.ApplyLayoutElementToNode(childYogaNode);
                }
                else if (layoutElement != null)
                {
                    UniFlexBoxNative.setNodeMinWidth(childYogaNode, layoutElement.minWidth);
                    UniFlexBoxNative.setNodeMinHeight(childYogaNode, layoutElement.minHeight);
                    UniFlexBoxNative.setNodeWidth(childYogaNode,
                        layoutElement.preferredWidth > 0
                            ? layoutElement.preferredWidth
                            : child.rect.width);

                    UniFlexBoxNative.setNodeHeight(childYogaNode,
                        layoutElement.preferredHeight > 0
                            ? layoutElement.preferredWidth
                            : child.rect.height);

                    if (layoutElement.flexibleWidth > 0
                        && _flexDirection != YGFlexDirection.Column
                        && _flexDirection != YGFlexDirection.ColumnReverse)
                    {
                        UniFlexBoxNative.setNodeFlex(childYogaNode, layoutElement.flexibleWidth);
                    }

                    if (layoutElement.flexibleHeight > 0
                        && _flexDirection != YGFlexDirection.Row
                        && _flexDirection != YGFlexDirection.RowReverse)
                    {
                        UniFlexBoxNative.setNodeFlex(childYogaNode, layoutElement.flexibleHeight);
                    }
                }
                else
                {
                    UniFlexBoxNative.setNodeWidth(childYogaNode, child.rect.width);
                    UniFlexBoxNative.setNodeHeight(childYogaNode, child.rect.height);
                }

                UniFlexBoxNative.addChild(_rootYogaNode, childYogaNode);
                _childNodes.Add(child, childYogaNode);
            }

            UniFlexBoxNative.calculateLayout(_rootYogaNode);

            ApplyNodeValuesToLayoutGroup();
            foreach (RectTransform child in _rectChildren)
            {
                ApplyNodeValuesToLayoutElement(_childNodes[child], child);
            }
        }

        private static ILayoutElement GetLayoutElementComponent(RectTransform child)
        {
            List<ILayoutElement> layoutElements =
                child.GetComponents<ILayoutElement>()
                    .Where(le => (le as Behaviour)?.isActiveAndEnabled ?? false)
                    .OrderByDescending(le => le.layoutPriority)
                    .ToList();

            if (layoutElements.Any(le => le is IUniFlexBoxLayoutElement))
            {
                return layoutElements.First(le => le is IUniFlexBoxLayoutElement);
            }

            return layoutElements.FirstOrDefault();
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
            var constraintsSet = new HashSet<ConstraintType>();
            foreach (DimensionConstraint dimensionConstraint in _dimensionConstraints)
            {
                if (constraintsSet.Add(dimensionConstraint.Type))
                {
                    continue;
                }

                Debug.LogWarning(
                    $"{nameof(DimensionConstraints)} contain multiple elements with the same " +
                    $"{nameof(DimensionConstraint.Type)}, the constraints are applied in order," +
                    " so the last one will prevail.",
                    this);
                break;
            }
        }
#endif

        public void AddDimensionConstraint(DimensionConstraint constraint)
        {
            UniFlexBoxLayoutUtility.AddConstraint(this, constraint);
        }

        public void RemoveDimensionConstraint(DimensionConstraint constraint)
        {
            UniFlexBoxLayoutUtility.RemoveConstraint(this, constraint);
        }

        public void RemoveDimensionConstraints(Predicate<DimensionConstraint> predicate)
        {
            UniFlexBoxLayoutUtility.RemoveConstraints(this, predicate);
        }

        public void RemoveDimensionConstraint(int index)
        {
            UniFlexBoxLayoutUtility.RemoveConstraint(this, index);
        }

        public void AddPaddingConstraint(PaddingConstraint constraint)
        {
            PaddingConstraints.Add(constraint);
            PaddingConstraints = PaddingConstraints;
        }

        public void RemovePaddingConstraint(PaddingConstraint constraint)
        {
            bool removed = PaddingConstraints.Remove(constraint);
            if (removed)
            {
                PaddingConstraints = PaddingConstraints;
            }
        }

        public void RemovePaddingConstraints(Predicate<PaddingConstraint> predicate)
        {
            int numberOfRemovedConstraints = PaddingConstraints.RemoveAll(predicate);
            if (numberOfRemovedConstraints > 0)
            {
                PaddingConstraints = PaddingConstraints;
            }
        }

        public void RemovePaddingConstraint(int index)
        {
            PaddingConstraints.RemoveAt(index);
            PaddingConstraints = PaddingConstraints;
        }

        public void AddGapConstraint(GapConstraint constraint)
        {
            GapConstraints.Add(constraint);
            GapConstraints = GapConstraints;
        }

        public void RemoveGapConstraint(GapConstraint constraint)
        {
            bool removed = GapConstraints.Remove(constraint);
            if (removed)
            {
                GapConstraints = GapConstraints;
            }
        }

        public void RemoveGapConstraints(Predicate<GapConstraint> predicate)
        {
            int numberOfRemovedConstraints = GapConstraints.RemoveAll(predicate);
            if (numberOfRemovedConstraints > 0)
            {
                GapConstraints = GapConstraints;
            }
        }

        public void RemoveGapConstraint(int index)
        {
            GapConstraints.RemoveAt(index);
            GapConstraints = GapConstraints;
        }
    }
}