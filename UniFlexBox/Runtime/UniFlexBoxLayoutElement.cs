using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxLayoutElement : UIBehaviour, IUniFlexBoxLayoutElement, ILayoutIgnorer
    {
        [SerializeField]
        private bool m_IgnoreLayout;

        public bool ignoreLayout
        {
            get => m_IgnoreLayout;
            set => SetProperty(ref m_IgnoreLayout, value);
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
        private YGAlign _alignSelf;

        public YGAlign AlignSelf
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

        [SerializeField]
        public int _order;

        public int Order
        {
            get => _order;
            set => _order = value;
        }

        [Tooltip("The base size in the dimension the flex layout is oriented.")]
        [SerializeField]
        public float _flexBasis;

        public float FlexBasis
        {
            get => _flexBasis;
            set => _flexBasis = value;
        }

        [Header("Constraints")]
        [Tooltip("If defined, overrides the rest of the width constraints.")]
        [SerializeField]
        private bool _wrapWidth;

        public bool WrapWidth
        {
            get => _wrapWidth;
            set => SetProperty(ref _wrapWidth, value);
        }

        [Tooltip("If defined, overrides the rest of the width constraints.")]
        [SerializeField]
        private bool _wrapHeight;

        public bool WrapHeight
        {
            get => _wrapHeight;
            set => SetProperty(ref _wrapHeight, value);
        }

        [SerializeField]
        private RectOffset _padding;

        public RectOffset Padding
        {
            get => _padding;
            set => SetProperty(ref _padding, value);
        }

        public float minWidth =>
            _dimensionConstraints.Any(dc => dc.Type == ConstraintType.MinimumWidth)
                ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumWidth).Value
                : 0f;

        public float preferredWidth { get; private set; }
        public float flexibleWidth { get; private set; }

        public float minHeight => _dimensionConstraints.Any(dc => dc.Type == ConstraintType.MinimumHeight)
            ? _dimensionConstraints.First(dc => dc.Type == ConstraintType.MinimumHeight).Value
            : 0f;

        public float preferredHeight { get; private set; }
        public float flexibleHeight { get; private set; }

        public int layoutPriority { get; private set; }

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
        }

#endif
        private void SetProperty<T>(ref T currentValue, T newValue)
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                return;
            currentValue = newValue;
            SetDirty();
        }

        public void AddConstraint(DimensionConstraint constraint)
        {
            UniFlexBoxLayoutElementExtensions.AddConstraint(this, constraint);
        }

        public void RemoveConstraint(DimensionConstraint constraint)
        {
            UniFlexBoxLayoutElementExtensions.RemoveConstraint(this, constraint);
        }

        public void RemoveConstraint(int index)
        {
            UniFlexBoxLayoutElementExtensions.RemoveConstraint(this, index);
        }
    }
}