#pragma once

#ifndef YOGA_API
#define YOGA_API __declspec(dllexport)
#endif

#include "yoga/Yoga.h"

extern "C" YOGA_API YGNodeRef createNewNode();
extern "C" YOGA_API void freeNode(YGNodeRef node);
extern "C" YOGA_API void addChild(YGNodeRef node, YGNodeRef child);
extern "C" YOGA_API void removeAllChildren(YGNodeRef node);
extern "C" YOGA_API void calculateLayout(YGNodeRef node);
extern "C" YOGA_API void setNodeWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMinWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMaxWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMinHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMaxHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setFlexDirection(YGNodeRef node, int flexDirection);
extern "C" YOGA_API void setAlignItems(YGNodeRef node, int alignItems);
extern "C" YOGA_API void setJustifyContent(YGNodeRef node, int justifyContent);
extern "C" YOGA_API float getNodeLeft(YGNodeRef node);
extern "C" YOGA_API float getNodeTop(YGNodeRef node);
extern "C" YOGA_API float getNodeWidth(YGNodeRef node);
extern "C" YOGA_API float getNodeHeight(YGNodeRef node);
