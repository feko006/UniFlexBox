using System;
using UnityEngine;

namespace Feko.UniFlexBox
{
    public static class UniFlexBoxLayoutElementExtensions
    {
        public static void ApplySizeDimensions(this IUniFlexBoxLayoutElement layoutElement,
            IntPtr node, RectTransform rectTransform)
        {
            ApplyMinimumWidthDimensions(node, layoutElement.MinimumWidth);
            ApplyExactWidthDimensions(node, layoutElement.ExactWidth);
            ApplyMaximumWidthDimensions(node, layoutElement.MaximumWidth);
            if (!layoutElement.MinimumWidth.Use
                && !layoutElement.ExactWidth.Use
                && !layoutElement.MaximumWidth.Use)
            {
                UniFlexBoxNative.setNodeWidth(node, rectTransform.rect.width);
            }

            ApplyMinimumHeightDimensions(node, layoutElement.MinimumHeight);
            ApplyExactHeightDimensions(node, layoutElement.ExactHeight);
            ApplyMaximumHeightDimensions(node, layoutElement.MaximumHeight);
            if (!layoutElement.MinimumHeight.Use
                && !layoutElement.ExactHeight.Use
                && !layoutElement.MaximumHeight.Use)
            {
                UniFlexBoxNative.setNodeHeight(node, rectTransform.rect.height);
            }
        }

        private static void ApplyMinimumWidthDimensions(IntPtr node, DimensionProperties minimumWidth)
        {
            UniFlexBoxNative.setNodeMinWidth(node, minimumWidth.Use ? minimumWidth.Size : -1);
        }

        private static void ApplyExactWidthDimensions(IntPtr node, DimensionProperties exactWidth)
        {
            UniFlexBoxNative.setNodeWidth(node, exactWidth.Use ? exactWidth.Size : -1);
        }

        private static void ApplyMaximumWidthDimensions(IntPtr node, DimensionProperties maximumWidth)
        {
            UniFlexBoxNative.setNodeMaxWidth(node, maximumWidth.Use ? maximumWidth.Size : -1);
        }

        private static void ApplyMinimumHeightDimensions(IntPtr node, DimensionProperties minimumHeight)
        {
            UniFlexBoxNative.setNodeMinHeight(node, minimumHeight.Use ? minimumHeight.Size : -1);
        }

        private static void ApplyExactHeightDimensions(IntPtr node, DimensionProperties exactHeight)
        {
            UniFlexBoxNative.setNodeHeight(node, exactHeight.Use ? exactHeight.Size : -1);
        }

        private static void ApplyMaximumHeightDimensions(IntPtr node, DimensionProperties maximumHeight)
        {
            UniFlexBoxNative.setNodeMaxHeight(node, maximumHeight.Use ? maximumHeight.Size : -1);
        }
    }
}