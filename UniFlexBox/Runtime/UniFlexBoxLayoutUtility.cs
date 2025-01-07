using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Feko.UniFlexBox
{
    public static class UniFlexBoxLayoutUtility
    {
        public static void ApplyLayoutElementToNode(
            this IUniFlexBoxLayoutElement layoutElement,
            IntPtr node,
            RectTransform rectTransform)
        {
            UniFlexBoxNative.setNodeAlignSelf(node, (int)layoutElement.AlignSelf);
            UniFlexBoxNative.setNodeFlex(node, layoutElement.Flex);
            UniFlexBoxNative.setNodeFlexGrow(node, layoutElement.FlexGrow);
            UniFlexBoxNative.setNodeFlexShrink(node, layoutElement.FlexShrink);
            UniFlexBoxNative.setNodeAspectRatio(node, layoutElement.AspectRatio);

            if (layoutElement.DimensionConstraints != null)
            {
                foreach (DimensionConstraint constraint in layoutElement.DimensionConstraints)
                {
                    _nativeMethods[(int)constraint.Type, (int)constraint.Unit](node, constraint.Value);
                }

                if (!layoutElement.DimensionConstraints.Any(dc =>
                        dc.Type == ConstraintType.MinimumWidth
                        || dc.Type == ConstraintType.ExactWidth
                        || dc.Type == ConstraintType.MaximumWidth
                        || dc.Type == ConstraintType.FlexBasis))
                {
                    UniFlexBoxNative.setNodeWidth(node, rectTransform.rect.width);
                }

                if (!layoutElement.DimensionConstraints.Any(dc =>
                        dc.Type == ConstraintType.MinimumHeight
                        || dc.Type == ConstraintType.ExactHeight
                        || dc.Type == ConstraintType.MaximumHeight
                        || dc.Type == ConstraintType.FlexBasis))
                {
                    UniFlexBoxNative.setNodeHeight(node, rectTransform.rect.height);
                }
            }
            else
            {
                UniFlexBoxNative.setNodeWidth(node, rectTransform.rect.width);
                UniFlexBoxNative.setNodeHeight(node, rectTransform.rect.height);
            }
        }

        public static void ValidateDimensionConstraints(
            List<DimensionConstraint> dimensionConstraints,
            UnityEngine.Object context)
        {
            var constraintsSet = new HashSet<ConstraintType>();
            foreach (DimensionConstraint dimensionConstraint in dimensionConstraints)
            {
                if (constraintsSet.Add(dimensionConstraint.Type))
                {
                    continue;
                }

                Debug.LogWarning(
                    $"{nameof(IUniFlexBoxLayoutElement.DimensionConstraints)} contain multiple elements with the same "
                    + $"{nameof(DimensionConstraint.Type)}, the constraints are applied in order,"
                    + " so the last one will prevail.",
                    context);
                break;
            }
        }

        public static void ApplyPaddingConstraintsToNode(
            this IUniFlexBoxLayoutGroup layoutGroup,
            IntPtr node)
        {
            if (layoutGroup.PaddingConstraints == null)
            {
                return;
            }

            foreach (PaddingConstraint paddingConstraint in layoutGroup.PaddingConstraints)
            {
                if (paddingConstraint.Unit == ConstraintUnit.Pixels)
                {
                    UniFlexBoxNative.setNodePadding(node, (int)paddingConstraint.Edge, paddingConstraint.Value);
                }
                else if (paddingConstraint.Unit == ConstraintUnit.Percent)
                {
                    UniFlexBoxNative.setNodePaddingPercent(
                        node,
                        (int)paddingConstraint.Edge,
                        paddingConstraint.Value);
                }
            }
        }

        public static void ValidatePaddingConstraints(
            List<PaddingConstraint> paddingConstraints,
            UnityEngine.Object context)
        {
            var constraintsSet = new HashSet<YGEdge>();
            foreach (PaddingConstraint paddingConstraint in paddingConstraints)
            {
                if (constraintsSet.Add(paddingConstraint.Edge))
                {
                    continue;
                }

                Debug.LogWarning(
                    $"{nameof(IUniFlexBoxLayoutGroup.PaddingConstraints)} contain multiple elements with the same "
                    + $"{nameof(PaddingConstraint.Edge)}, the constraints are applied in order,"
                    + " so the last one will prevail.",
                    context);
                break;
            }
        }

        public static void ApplyGapConstraintsToNode(
            this IUniFlexBoxLayoutGroup layoutGroup,
            IntPtr node)
        {
            if (layoutGroup.GapConstraints == null)
            {
                return;
            }

            foreach (GapConstraint gapConstraint in layoutGroup.GapConstraints)
            {
                if (gapConstraint.Unit == ConstraintUnit.Pixels)
                {
                    UniFlexBoxNative.setNodeGap(node, (int)gapConstraint.Gutter, gapConstraint.Value);
                }
                else if (gapConstraint.Unit == ConstraintUnit.Percent)
                {
                    UniFlexBoxNative.setNodeGapPercent(node, (int)gapConstraint.Gutter, gapConstraint.Value);
                }
            }
        }

        public static void ValidateGapConstraints(
            List<GapConstraint> gapConstraints,
            UnityEngine.Object context)
        {
            var constraintsSet = new HashSet<YGGutter>();
            foreach (GapConstraint gapConstraint in gapConstraints)
            {
                if (constraintsSet.Add(gapConstraint.Gutter))
                {
                    continue;
                }

                Debug.LogWarning(
                    $"{nameof(IUniFlexBoxLayoutGroup.GapConstraints)} contain multiple elements with the same "
                    + $"{nameof(GapConstraint.Gutter)}, the constraints are applied in order,"
                    + " so the last one will prevail.",
                    context);
                break;
            }
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
            {
                // Flex Basis
                // Auto
                (node, value) => UniFlexBoxNative.setNodeFlexBasisAuto(node),
                // Stretch
                (node, value) => UniFlexBoxNative.setNodeFlexBasisStretch(node),
                // FitContent
                (node, value) => UniFlexBoxNative.setNodeFlexBasisFitContent(node),
                // MaxContent
                (node, value) => UniFlexBoxNative.setNodeFlexBasisMaxContent(node),
                // Units
                UniFlexBoxNative.setNodeFlexBasis,
                // Percent
                UniFlexBoxNative.setNodeFlexBasisPercent,
            },
        };
    }
}