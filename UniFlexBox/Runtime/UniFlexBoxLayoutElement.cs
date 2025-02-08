using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    [DisallowMultipleComponent]
    public class UniFlexBoxLayoutElement : UIBehaviour, IUniFlexBoxLayoutElement, ILayoutIgnorer
    {
        [SerializeField]
        private bool _ignoreLayout;

        public bool ignoreLayout
        {
            get => _ignoreLayout;
            set => SetProperty(ref _ignoreLayout, value);
        }

        [SerializeField]
        private int _layoutPriority;

        public int layoutPriority
        {
            get => _layoutPriority;
            set => SetProperty(ref _layoutPriority, value);
        }

        [SerializeField]
        private float _flex;

        public float Flex
        {
            get => _flex;
            set => SetProperty(ref _flex, value);
        }

        [SerializeField]
        public float _flexGrow;

        public float FlexGrow
        {
            get => _flexGrow;
            set => _flexGrow = value;
        }

        [SerializeField]
        public float _flexShrink;

        public float FlexShrink
        {
            get => _flexShrink;
            set => _flexShrink = value;
        }

        [SerializeField]
        private float _aspectRatio;

        public float AspectRatio
        {
            get => _aspectRatio;
            set => SetProperty(ref _aspectRatio, value);
        }

        [SerializeField]
        private YGAlignItems _alignSelf = YGAlignItems.FlexStart;

        public YGAlignItems AlignSelf
        {
            get => _alignSelf;
            set => SetProperty(ref _alignSelf, value);
        }

        [SerializeField]
        private List<DimensionConstraint> _dimensionConstraints;

        public List<DimensionConstraint> DimensionConstraints
        {
            get => _dimensionConstraints;
            set => SetProperty(ref _dimensionConstraints, value);
        }

        public float minWidth =>
            _dimensionConstraints.Any(dc => dc.Type == ConstraintType.MinimumWidth)
                ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumWidth).Value
                : 0f;

        public float preferredWidth { get; internal set; }
        public float flexibleWidth { get; private set; }

        public float minHeight => _dimensionConstraints.Any(dc => dc.Type == ConstraintType.MinimumHeight)
            ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumHeight).Value
            : 0f;

        public float preferredHeight { get; internal set; }
        public float flexibleHeight { get; private set; }

        public virtual void CalculateLayoutInputHorizontal() { }

        public virtual void CalculateLayoutInputVertical() { }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetDirty();
        }

        protected override void OnTransformParentChanged()
        {
            SetDirty();
        }

        protected override void OnDisable()
        {
            SetDirty();
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            SetDirty();
        }

        protected override void OnBeforeTransformParentChanged()
        {
            SetDirty();
        }

        /// <summary>
        /// Mark the LayoutElement as dirty.
        /// </summary>
        /// <remarks>
        /// This will make the auto layout system process this element on the next layout pass. This method should be called by the LayoutElement whenever a change is made that potentially affects the layout.
        /// </remarks>
        private void SetDirty()
        {
            if (!IsActive())
                return;
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
            UniFlexBoxLayoutUtility.ValidateDimensionConstraints(_dimensionConstraints, gameObject);
        }

#endif
        private void SetProperty<T>(ref T currentValue, T newValue)
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                return;
            currentValue = newValue;
            SetDirty();
        }

        public void NotifyDimensionConstraintsChanged()
        {
            DimensionConstraints = DimensionConstraints;
        }
    }
}