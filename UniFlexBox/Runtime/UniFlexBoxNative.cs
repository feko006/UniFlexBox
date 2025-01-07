using System;
using System.Runtime.InteropServices;

namespace Feko.UniFlexBox
{
    public static class UniFlexBoxNative
    {
        [DllImport("uniflexbox-native")]
        public static extern IntPtr createNewNode();

        [DllImport("uniflexbox-native")]
        public static extern void resetNode(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void freeNodeRecursive(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void addChild(IntPtr node, IntPtr child);

        [DllImport("uniflexbox-native")]
        public static extern void removeAllChildren(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void calculateLayout(IntPtr node);

        #region Width

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidth(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidthPercent(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidthAuto(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidthMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidthFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidthStretch(IntPtr node);

        #endregion Width

        #region MinWidth

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinWidth(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinWidthPercent(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinWidthMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinWidthFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinWidthStretch(IntPtr node);

        #endregion MinWidth

        #region MaxWidth

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxWidth(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxWidthPercent(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxWidthMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxWidthFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxWidthStretch(IntPtr node);

        #endregion MaxWidth

        #region Height

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeight(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeightPercent(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeightAuto(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeightMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeightFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeightStretch(IntPtr node);

        #endregion Height

        #region MinHeight

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinHeight(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinHeightPercent(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinHeightMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinHeightFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMinHeightStretch(IntPtr node);

        #endregion MinHeight

        #region MaxHeight

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxHeight(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxHeightPercent(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxHeightMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxHeightFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeMaxHeightStretch(IntPtr node);

        #endregion MaxHeight

        #region FlexBasis

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasis(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasisPercent(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasisAuto(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasisMaxContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasisFitContent(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexBasisStretch(IntPtr node);

        #endregion FlexBasis

        #region Padding & Gap

        [DllImport("uniflexbox-native")]
        public static extern void setNodePadding(IntPtr node, int edge, float padding);

        [DllImport("uniflexbox-native")]
        public static extern void setNodePaddingPercent(IntPtr node, int edge, float padding);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeGap(IntPtr node, int gutter, float gapLength);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeGapPercent(IntPtr node, int gutter, float gapLength);

        #endregion Padding & Gap

        #region Style

        [DllImport("uniflexbox-native")]
        public static extern void copyNodeStyle(IntPtr dstNode, IntPtr srcNode);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeDirection(IntPtr node, int direction);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexDirection(IntPtr node, int flexDirection);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeJustifyContent(IntPtr rootYogaNode, int justifyContent);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeAlignContent(IntPtr node, int alignContent);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeAlignItems(IntPtr node, int alignItems);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeAlignSelf(IntPtr node, int alignSelf);

        [DllImport("uniflexbox-native")]
        public static extern void setNodePositionType(IntPtr node, int positionType);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexWrap(IntPtr node, int flexWrap);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeOverflow(IntPtr node, int overflow);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeDisplay(IntPtr node, int display);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlex(IntPtr node, float flex);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexGrow(IntPtr node, float flexGrow);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeFlexShrink(IntPtr node, float flexShrink);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeAspectRatio(IntPtr node, float aspectRatio);

        #endregion Style

        [DllImport("uniflexbox-native")]
        public static extern float getNodeLeft(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern float getNodeTop(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern float getNodeWidth(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern float getNodeHeight(IntPtr node);
    }
}