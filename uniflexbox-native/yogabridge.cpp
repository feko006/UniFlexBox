#include "pch.h"
#include "yogabridge.h"

YGNodeRef createNewNode()
{
    return YGNodeNew();
}

void freeNode(const YGNodeRef node)
{
    YGNodeFree(node);
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
    YGNodeStyleSetMaxWidth(node, width >= 0 ? width : YGUndefined);
}

void setNodeMaxWidthPercent(const YGNodeRef node, const float width)
{
    YGNodeStyleSetMaxWidthPercent(node, width >= 0 ? width : YGUndefined);
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
    YGNodeStyleSetMaxHeight(node, height >= 0 ? height : YGUndefined);
}

void setNodeMaxHeightPercent(const YGNodeRef node, const float height)
{
    YGNodeStyleSetMaxHeightPercent(node, height >= 0 ? height : YGUndefined);
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

void setFlexDirection(const YGNodeRef node, const int flexDirection)
{
    YGNodeStyleSetFlexDirection(node, static_cast<YGFlexDirection>(flexDirection));
}

void setAlignItems(const YGNodeRef node, const int alignItems)
{
    YGNodeStyleSetAlignItems(node, static_cast<YGAlign>(alignItems));
}

void setJustifyContent(const YGNodeRef node, const int justifyContent)
{
    YGNodeStyleSetJustifyContent(node, static_cast<YGJustify>(justifyContent));
}

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
