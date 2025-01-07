#include "pch.h"
#include "yogabridge.h"

YGNodeRef createNewNode()
{
    return YGNodeNew();
}

void resetNode(const YGNodeRef node)
{
    YGNodeReset(node);
}

void freeNodeRecursive(const YGNodeRef node)
{
    YGNodeFreeRecursive(node);
}

void addChild(const YGNodeRef node, const YGNodeRef child)
{
    YGNodeInsertChild(node, child, YGNodeGetChildCount(node));
}

void removeAllChildren(const YGNodeRef node)
{
    YGNodeRemoveAllChildren(node);
}

void calculateLayout(const YGNodeRef node)
{
    YGNodeCalculateLayout(node, YGUndefined, YGUndefined, YGDirectionLTR);
}

#pragma region Width

void setNodeWidth(const YGNodeRef node, const float width)
{
    YGNodeStyleSetWidth(node, width >= 0 ? width : YGUndefined);
}

void setNodeWidthPercent(const YGNodeRef node, const float width)
{
    YGNodeStyleSetWidthPercent(node, width >= 0 ? width : YGUndefined);
}

void setNodeWidthAuto(const YGNodeRef node)
{
    YGNodeStyleSetWidthAuto(node);
}

void setNodeWidthMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetWidthMaxContent(node);
}

void setNodeWidthFitContent(const YGNodeRef node)
{
    YGNodeStyleSetWidthFitContent(node);
}

void setNodeWidthStretch(const YGNodeRef node)
{
    YGNodeStyleSetWidthStretch(node);
}

#pragma endregion Width

#pragma region MinWidth

void setNodeMinWidth(const YGNodeRef node, const float width)
{
    YGNodeStyleSetMinWidth(node, width >= 0 ? width : YGUndefined);
}

void setNodeMinWidthPercent(const YGNodeRef node, const float width)
{
    YGNodeStyleSetMinWidthPercent(node, width >= 0 ? width : YGUndefined);
}

void setNodeMinWidthMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetMinWidthMaxContent(node);
}

void setNodeMinWidthFitContent(const YGNodeRef node)
{
    YGNodeStyleSetMinWidthFitContent(node);
}

void setNodeMinWidthStretch(const YGNodeRef node)
{
    YGNodeStyleSetMinWidthStretch(node);
}

#pragma endregion MinWidth

#pragma region MaxWidth

void setNodeMaxWidth(const YGNodeRef node, const float width)
{
    YGNodeStyleSetMaxWidth(node, width > 0 ? width : YGUndefined);
}

void setNodeMaxWidthPercent(const YGNodeRef node, const float width)
{
    YGNodeStyleSetMaxWidthPercent(node, width > 0 ? width : YGUndefined);
}

void setNodeMaxWidthMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetMaxWidthMaxContent(node);
}

void setNodeMaxWidthFitContent(const YGNodeRef node)
{
    YGNodeStyleSetMaxWidthFitContent(node);
}

void setNodeMaxWidthStretch(const YGNodeRef node)
{
    YGNodeStyleSetMaxWidthStretch(node);
}

#pragma endregion MaxWidth

#pragma region Height

void setNodeHeight(const YGNodeRef node, const float height)
{
    YGNodeStyleSetHeight(node, height >= 0 ? height : YGUndefined);
}

void setNodeHeightPercent(const YGNodeRef node, const float height)
{
    YGNodeStyleSetHeightPercent(node, height >= 0 ? height : YGUndefined);
}

void setNodeHeightAuto(const YGNodeRef node)
{
    YGNodeStyleSetHeightAuto(node);
}

void setNodeHeightMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetHeightMaxContent(node);
}

void setNodeHeightFitContent(const YGNodeRef node)
{
    YGNodeStyleSetHeightFitContent(node);
}

void setNodeHeightStretch(const YGNodeRef node)
{
    YGNodeStyleSetHeightStretch(node);
}

#pragma endregion Height

#pragma region MinHeight

void setNodeMinHeight(const YGNodeRef node, const float height)
{
    YGNodeStyleSetMinHeight(node, height >= 0 ? height : YGUndefined);
}

void setNodeMinHeightPercent(const YGNodeRef node, const float height)
{
    YGNodeStyleSetMinHeightPercent(node, height >= 0 ? height : YGUndefined);
}

void setNodeMinHeightMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetMinHeightMaxContent(node);
}

void setNodeMinHeightFitContent(const YGNodeRef node)
{
    YGNodeStyleSetMinHeightFitContent(node);
}

void setNodeMinHeightStretch(const YGNodeRef node)
{
    YGNodeStyleSetMinHeightStretch(node);
}

#pragma endregion MinHeight

#pragma region MaxHeight

void setNodeMaxHeight(const YGNodeRef node, const float height)
{
    YGNodeStyleSetMaxHeight(node, height > 0 ? height : YGUndefined);
}

void setNodeMaxHeightPercent(const YGNodeRef node, const float height)
{
    YGNodeStyleSetMaxHeightPercent(node, height > 0 ? height : YGUndefined);
}

void setNodeMaxHeightMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetMaxHeightMaxContent(node);
}

void setNodeMaxHeightFitContent(const YGNodeRef node)
{
    YGNodeStyleSetMaxHeightFitContent(node);
}

void setNodeMaxHeightStretch(const YGNodeRef node)
{
    YGNodeStyleSetMaxHeightStretch(node);
}

#pragma endregion MaxHeight

#pragma region FlexBasis

void setNodeFlexBasis(const YGNodeRef node, const float width)
{
    YGNodeStyleSetFlexBasis(node, width >= 0 ? width : YGUndefined);
}

void setNodeFlexBasisPercent(const YGNodeRef node, const float width)
{
    YGNodeStyleSetFlexBasisPercent(node, width >= 0 ? width : YGUndefined);
}

void setNodeFlexBasisAuto(const YGNodeRef node)
{
    YGNodeStyleSetFlexBasisAuto(node);
}

void setNodeFlexBasisMaxContent(const YGNodeRef node)
{
    YGNodeStyleSetFlexBasisMaxContent(node);
}

void setNodeFlexBasisFitContent(const YGNodeRef node)
{
    YGNodeStyleSetFlexBasisFitContent(node);
}

void setNodeFlexBasisStretch(const YGNodeRef node)
{
    YGNodeStyleSetFlexBasisStretch(node);
}

#pragma endregion FlexBasis

#pragma region Padding & Gap

void setNodePadding(const YGNodeRef node, const YGEdge edge, const float padding)
{
    YGNodeStyleSetPadding(node, edge, padding >= 0 ? padding : YGUndefined);
}

void setNodePaddingPercent(const YGNodeRef node, const YGEdge edge, const float padding)
{
    YGNodeStyleSetPaddingPercent(node, edge, padding >= 0 ? padding : YGUndefined);
}

void setNodeGap(const YGNodeRef node, const YGGutter gutter, const float gapLength)
{
    YGNodeStyleSetGap(node, gutter, gapLength >= 0 ? gapLength : YGUndefined);
}

void setNodeGapPercent(const YGNodeRef node, const YGGutter gutter, const float gapLength)
{
    YGNodeStyleSetGapPercent(node, gutter, gapLength >= 0 ? gapLength : YGUndefined);
}

#pragma endregion Padding & Gap

#pragma region Style

void copyNodeStyle(const YGNodeRef dstNode, const YGNodeRef srcNode)
{
    YGNodeCopyStyle(dstNode, srcNode);
}

void setNodeDirection(const YGNodeRef node, const int direction)
{
    YGNodeStyleSetDirection(node, static_cast<YGDirection>(direction));
}

void setNodeFlexDirection(const YGNodeRef node, const int flexDirection)
{
    YGNodeStyleSetFlexDirection(node, static_cast<YGFlexDirection>(flexDirection));
}

void setNodeJustifyContent(const YGNodeRef node, const int justifyContent)
{
    YGNodeStyleSetJustifyContent(node, static_cast<YGJustify>(justifyContent));
}

void setNodeAlignContent(const YGNodeRef node, const int alignContent)
{
    YGNodeStyleSetAlignContent(node, static_cast<YGAlign>(alignContent));
}

void setNodeAlignItems(const YGNodeRef node, const int alignItems)
{
    YGNodeStyleSetAlignItems(node, static_cast<YGAlign>(alignItems));
}

void setNodeAlignSelf(const YGNodeRef node, const int alignSelf)
{
    YGNodeStyleSetAlignSelf(node, static_cast<YGAlign>(alignSelf));
}

void setNodePositionType(const YGNodeRef node, const int positionType)
{
    YGNodeStyleSetPositionType(node, static_cast<YGPositionType>(positionType));
}

void setNodeFlexWrap(const YGNodeRef node, const int flexWrap)
{
    YGNodeStyleSetFlexWrap(node, static_cast<YGWrap>(flexWrap));
}

void setNodeOverflow(const YGNodeRef node, const int overflow)
{
    YGNodeStyleSetOverflow(node, static_cast<YGOverflow>(overflow));
}

void setNodeDisplay(const YGNodeRef node, const int display)
{
    YGNodeStyleSetDisplay(node, static_cast<YGDisplay>(display));
}

void setNodeFlex(const YGNodeRef node, const float flex)
{
    YGNodeStyleSetFlex(node, flex);
}

void setNodeFlexGrow(const YGNodeRef node, const float flexGrow)
{
    YGNodeStyleSetFlexGrow(node, flexGrow);
}

void setNodeFlexShrink(const YGNodeRef node, const float flexShrink)
{
    YGNodeStyleSetFlexShrink(node, flexShrink);
}

void setNodeAspectRatio(const YGNodeRef node, const float aspectRatio)
{
    YGNodeStyleSetAspectRatio(node, aspectRatio > 0 ? aspectRatio : YGUndefined);
}

#pragma endregion Style

float getNodeLeft(const YGNodeRef node)
{
    return YGNodeLayoutGetLeft(node);
}

float getNodeTop(const YGNodeRef node)
{
    return YGNodeLayoutGetTop(node);
}

float getNodeWidth(const YGNodeRef node)
{
    return YGNodeLayoutGetWidth(node);
}

float getNodeHeight(const YGNodeRef node)
{
    return YGNodeLayoutGetHeight(node);
}
