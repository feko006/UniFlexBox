using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxLayoutGroup : UIBehaviour, ILayoutElement, ILayoutGroup
    {
        [Header("Flex Properties")]
        [SerializeField]
        private FlexDirection _flexDirection;
        public FlexDirection FlexDirection { get { return _flexDirection; } set { SetProperty(ref _flexDirection, value); } }

        [SerializeField]
        private FlexWrap _flexWrap;
        public FlexWrap FlexWrap { get { return _flexWrap; } set { SetProperty(ref _flexWrap, value); } }

        [SerializeField]
        private JustifyContent _justifyContent;
        public JustifyContent JustifyContent { get { return _justifyContent; } set { SetProperty(ref _justifyContent, value); } }

        [SerializeField]
        private AlignItems _alignItems;
        public AlignItems AlignItems { get { return _alignItems; } set { SetProperty(ref _alignItems, value); } }

        [SerializeField]
        private AlignContent _alignContent;
        public AlignContent AlignContent { get { return _alignContent; } set { SetProperty(ref _alignContent, value); } }

        [Header("Constraints")]
        [Tooltip("If defined, overrides the minimum and maximum width constraints.")]
        [SerializeField]
        private float _definiteWidth;
        [SerializeField]
        private float _minimumWidth;
        [SerializeField]
        private float _maximumWidth;

        [SerializeField]
        private RectOffset _padding;
        public RectOffset Padding { get { return _padding; } set { SetProperty(ref _padding, value); } }


        [Tooltip("If defined, overrides the minimum and maximum height constraints.")]
        [SerializeField]
        private float _definiteHeight;
        [SerializeField]
        private float _minimumHeight;
        [SerializeField]
        private float _maximumHeight;

        public float minWidth { get; private set; }
        public float preferredWidth { get; private set; }
        public float flexibleWidth { get; private set; }
        public float minHeight { get; private set; }
        public float preferredHeight { get; private set; }
        public float flexibleHeight { get; private set; }
        public int layoutPriority { get; private set; }

        private readonly List<RectTransform> _rectChildren = new List<RectTransform>();

        [System.NonSerialized]
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

        public void CalculateLayoutInputHorizontal()
        {
            UpdateChildren();
            _tracker.Clear();
            float[] containerWidth = CalculateContainerSize(_definiteWidth, _minimumWidth, ref _maximumWidth, 0);
            float[] availableHorizontalSpace = CalculateAvailableSpace(containerWidth, 0);
            float[] containerHeight = CalculateContainerSize(_definiteHeight, _minimumHeight, ref _maximumHeight, 1);
            float[] availableVerticalSpace = CalculateAvailableSpace(containerHeight, 1);
            Debug.Log($"UniFlexBox: Container size, horizontal: ({containerWidth[0]}, {containerWidth[1]}), vertical: ({containerHeight[0]}, {containerHeight[1]})");
            Debug.Log($"UniFlexBox: Available space, horizontal: ({availableHorizontalSpace[0]}, {availableHorizontalSpace[1]}), vertical: ({availableVerticalSpace[0]}, {availableVerticalSpace[1]})");
        }

        public void CalculateLayoutInputVertical() { }

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

        public void SetLayoutHorizontal() { }
        public void SetLayoutVertical() { }

        /// <summary>
        /// Helper method used to set a given property if it has changed.
        /// </summary>
        /// <param name="currentValue">A reference to the member value.</param>
        /// <param name="newValue">The new value.</param>
        protected void SetProperty<T>(ref T currentValue, T newValue)
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
                return;
            currentValue = newValue;
            SetDirty();
        }

        /// <summary>
        /// Mark the LayoutGroup as dirty.
        /// </summary>
        protected void SetDirty()
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
