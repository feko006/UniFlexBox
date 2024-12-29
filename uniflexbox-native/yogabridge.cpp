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

void setNodeWidth(const YGNodeRef node, const float width)
{
    YGNodeStyleSetWidth(node, width);
}

void setNodeHeight(const YGNodeRef node, const float height)
{
    YGNodeStyleSetHeight(node, height);
}

void setFlexDirection(const YGNodeRef node, const int flexDirection)
{
    YGNodeStyleSetFlexDirection(node, static_cast<YGFlexDirection>(flexDirection));
}

void setAlignItems(const YGNodeRef node, const int alignItems)
{
    YGNodeStyleSetAlignItems(node, static_cast<YGAlign>(alignItems));
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
