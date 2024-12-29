using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxItem : UIBehaviour, ILayoutElement, ILayoutIgnorer
    {
        [SerializeField] private bool m_IgnoreLayout = false;

        public bool ignoreLayout
        {
            get { return m_IgnoreLayout; }
            set { SetProperty(ref m_IgnoreLayout, value); }
        }

        [SerializeField] public int _order;

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        [SerializeField] public Flex _flex;

        public Flex Flex
        {
            get { return _flex; }
            set { _flex = value; }
        }

        [SerializeField] public float _flexGrow;

        public float FlexGrow
        {
            get { return _flexGrow; }
            set { _flexGrow = value; }
        }

        [SerializeField] public float _flexShrink;

        public float FlexShrink
        {
            get { return _flexShrink; }
            set { _flexShrink = value; }
        }

        [Tooltip("The base size in the dimension the flex layout is oriented.")] [SerializeField]
        public float _flexBasis;

        public float FlexBasis
        {
            get { return _flexBasis; }
            set { _flexBasis = value; }
        }

        [SerializeField] public AlignSelf _alignSelf;

        public AlignSelf AlignSelf
        {
            get { return _alignSelf; }
            set { _alignSelf = value; }
        }

        [Header("Constraints")] [Tooltip("If defined, overrides the rest of the width constraints.")] [SerializeField]
        private bool _wrapWidth;

        public bool WrapWidth
        {
            get { return _wrapWidth; }
            set
            {
                SetProperty(ref _wrapWidth, value);
                ;
            }
        }

        [Tooltip("If defined, overrides the minimum and maximum width constraints.")] [SerializeField]
        private float _definiteWidth;

        public float DefiniteWidth
        {
            get { return _definiteWidth; }
            set { SetProperty(ref _definiteWidth, value); }
        }

        [SerializeField] private float _minimumWidth;

        public float MinimumWidth
        {
            get { return _minimumWidth; }
            set { SetProperty(ref _minimumWidth, value); }
        }

        [SerializeField] private float _maximumWidth;

        public float MaximumWidth
        {
            get { return _maximumWidth; }
            set { SetProperty(ref _maximumWidth, value); }
        }

        [Tooltip("If defined, overrides the rest of the width constraints.")] [SerializeField]
        private bool _wrapHeight;

        public bool WrapHeight
        {
            get { return _wrapHeight; }
            set
            {
                SetProperty(ref _wrapHeight, value);
                ;
            }
        }

        [Tooltip("If defined, overrides the minimum and maximum height constraints.")] [SerializeField]
        private float _definiteHeight;

        public float DefiniteHeight
        {
            get { return _definiteHeight; }
            set { SetProperty(ref _definiteHeight, value); }
        }

        [SerializeField] private float _minimumHeight;

        public float MinimumHeight
        {
            get { return _minimumHeight; }
            set { SetProperty(ref _minimumHeight, value); }
        }

        [SerializeField] private float _maximumHeight;

        public float MaximumHeight
        {
            get { return _maximumHeight; }
            set { SetProperty(ref _maximumHeight, value); }
        }

        [SerializeField] private RectOffset _padding;

        public RectOffset Padding
        {
            get { return _padding; }
            set { SetProperty(ref _padding, value); }
        }

        public float minWidth { get; private set; }
        public float preferredWidth { get; private set; }
        public float flexibleWidth { get; private set; }
        public float minHeight { get; private set; }
        public float preferredHeight { get; private set; }
        public float flexibleHeight { get; private set; }
        public int layoutPriority { get; private set; }

        public virtual void CalculateLayoutInputHorizontal()
        {
        }

        public virtual void CalculateLayoutInputVertical()
        {
        }

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
    }
}