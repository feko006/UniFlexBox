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

#pragma region Width

extern "C" YOGA_API void setNodeWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeWidthPercent(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeWidthAuto(YGNodeRef node);
extern "C" YOGA_API void setNodeWidthMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeWidthFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeWidthStretch(YGNodeRef node);

#pragma endregion Width

#pragma region MinWidth

extern "C" YOGA_API void setNodeMinWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMinWidthPercent(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMinWidthMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMinWidthFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMinWidthStretch(YGNodeRef node);

#pragma endregion MinWidth

#pragma region MaxWidth

extern "C" YOGA_API void setNodeMaxWidth(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMaxWidthPercent(YGNodeRef node, float width);
extern "C" YOGA_API void setNodeMaxWidthMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMaxWidthFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMaxWidthStretch(YGNodeRef node);

#pragma endregion MaxWidth

#pragma region Height

extern "C" YOGA_API void setNodeHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeHeightPercent(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeHeightAuto(YGNodeRef node);
extern "C" YOGA_API void setNodeHeightMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeHeightFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeHeightStretch(YGNodeRef node);

#pragma endregion Height

#pragma region MinHeight

extern "C" YOGA_API void setNodeMinHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMinHeightPercent(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMinHeightMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMinHeightFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMinHeightStretch(YGNodeRef node);

#pragma endregion MinHeight

#pragma region MaxHeight

extern "C" YOGA_API void setNodeMaxHeight(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMaxHeightPercent(YGNodeRef node, float height);
extern "C" YOGA_API void setNodeMaxHeightMaxContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMaxHeightFitContent(YGNodeRef node);
extern "C" YOGA_API void setNodeMaxHeightStretch(YGNodeRef node);

#pragma endregion MaxHeight

extern "C" YOGA_API void setFlexDirection(YGNodeRef node, int flexDirection);
extern "C" YOGA_API void setAlignItems(YGNodeRef node, int alignItems);
extern "C" YOGA_API void setJustifyContent(YGNodeRef node, int justifyContent);
extern "C" YOGA_API float getNodeLeft(YGNodeRef node);
extern "C" YOGA_API float getNodeTop(YGNodeRef node);
extern "C" YOGA_API float getNodeWidth(YGNodeRef node);
extern "C" YOGA_API float getNodeHeight(YGNodeRef node);
