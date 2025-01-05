using System;

namespace Feko.UniFlexBox
{
    public static class UniFlexBoxLayoutElementExtensions
    {
        public static void ApplyLayoutElementToNode(
            this IUniFlexBoxLayoutElement layoutElement,
            IntPtr node)
        {
            UniFlexBoxNative.setNodeAlignSelf(node, (int)layoutElement.AlignSelf);
            UniFlexBoxNative.setNodeFlex(node, layoutElement.Flex);
            UniFlexBoxNative.setNodeFlexGrow(node, layoutElement.FlexGrow);
            UniFlexBoxNative.setNodeFlexShrink(node, layoutElement.FlexShrink);

            if (layoutElement.DimensionConstraints == null)
            {
                return;
            }

            foreach (DimensionConstraint constraint in layoutElement.DimensionConstraints)
            {
                _nativeMethods[(int)constraint.Type, (int)constraint.Unit](node, constraint.Value);
            }
        }

        public static void AddConstraint(
            this IUniFlexBoxLayoutElement layoutElement,
            DimensionConstraint constraint)
        {
            layoutElement.DimensionConstraints.Add(constraint);
            layoutElement.DimensionConstraints = layoutElement.DimensionConstraints;
        }

        public static void RemoveConstraint(
            this IUniFlexBoxLayoutElement layoutElement,
            DimensionConstraint constraint)
        {
            bool removed = layoutElement.DimensionConstraints.Remove(constraint);
            if (removed)
            {
                layoutElement.DimensionConstraints = layoutElement.DimensionConstraints;
            }
        }

        public static void RemoveConstraint(
            this IUniFlexBoxLayoutElement layoutElement,
            int index)
        {
            layoutElement.DimensionConstraints.RemoveAt(index);
            layoutElement.DimensionConstraints = layoutElement.DimensionConstraints;
        }

        private static readonly Action<IntPtr, float>[,] _nativeMethods =
        {
            {
                // Minimum Width
                // Auto
                (node, value) => { }, // No Auto variant for MinWidth
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeMinWidthStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeMinWidthFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeMinWidthMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeMinWidth,
                // Percent
                UniFlexBoxNative.setNodeMinWidthPercent,
            },
            {
                // Width
                // Auto
                (node, value) => UniFlexBoxNative.setNodeWidthAuto(node),
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeWidthStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeWidthFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeWidthMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeWidth,
                // Percent
                UniFlexBoxNative.setNodeWidthPercent,
            },
            {
                // Maximum Width
                // Auto
                (node, value) => { }, // No Auto variant for MaxWidth
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeMaxWidthStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeMaxWidthFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeMaxWidthMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeMaxWidth,
                // Percent
                UniFlexBoxNative.setNodeMaxWidthPercent,
            },
            {
                // Minimum Height
                // Auto
                (node, value) => { }, // No Auto variant for MinHeight
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeMinHeightStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeMinHeightFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeMinHeightMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeMinHeight,
                // Percent
                UniFlexBoxNative.setNodeMinHeightPercent,
            },
            {
                // Height
                // Auto
                (node, value) => UniFlexBoxNative.setNodeHeightAuto(node),
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeHeightStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeHeightFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeHeightMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeHeight,
                // Percent
                UniFlexBoxNative.setNodeHeightPercent,
            },
            {
                // Maximum Height
                // Auto
                (node, value) => { }, // No Auto variant for MaxHeight
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeMaxHeightStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeMaxHeightFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeMaxHeightMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeMaxHeight,
                // Percent
                UniFlexBoxNative.setNodeMaxHeightPercent,
            },
        };
    }
}