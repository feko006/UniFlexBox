#pragma once

#ifndef YOGA_API
#define YOGA_API __declspec(dllexport)
#endif

#include "yoga/Yoga.h"

extern "C" {
YOGA_API YGNodeRef createNewNode();
YOGA_API void resetNode(YGNodeRef node);
YOGA_API void freeNode(YGNodeRef node);
YOGA_API void addChild(YGNodeRef node, YGNodeRef child);
YOGA_API void removeAllChildren(YGNodeRef node);
YOGA_API void calculateLayout(YGNodeRef node);

#pragma region Width

YOGA_API void setNodeWidth(YGNodeRef node, float width);
YOGA_API void setNodeWidthPercent(YGNodeRef node, float width);
YOGA_API void setNodeWidthAuto(YGNodeRef node);
YOGA_API void setNodeWidthMaxContent(YGNodeRef node);
YOGA_API void setNodeWidthFitContent(YGNodeRef node);
YOGA_API void setNodeWidthStretch(YGNodeRef node);

#pragma endregion Width

#pragma region MinWidth

YOGA_API void setNodeMinWidth(YGNodeRef node, float width);
YOGA_API void setNodeMinWidthPercent(YGNodeRef node, float width);
YOGA_API void setNodeMinWidthMaxContent(YGNodeRef node);
YOGA_API void setNodeMinWidthFitContent(YGNodeRef node);
YOGA_API void setNodeMinWidthStretch(YGNodeRef node);

#pragma endregion MinWidth

#pragma region MaxWidth

YOGA_API void setNodeMaxWidth(YGNodeRef node, float width);
YOGA_API void setNodeMaxWidthPercent(YGNodeRef node, float width);
YOGA_API void setNodeMaxWidthMaxContent(YGNodeRef node);
YOGA_API void setNodeMaxWidthFitContent(YGNodeRef node);
YOGA_API void setNodeMaxWidthStretch(YGNodeRef node);

#pragma endregion MaxWidth

#pragma region Height

YOGA_API void setNodeHeight(YGNodeRef node, float height);
YOGA_API void setNodeHeightPercent(YGNodeRef node, float height);
YOGA_API void setNodeHeightAuto(YGNodeRef node);
YOGA_API void setNodeHeightMaxContent(YGNodeRef node);
YOGA_API void setNodeHeightFitContent(YGNodeRef node);
YOGA_API void setNodeHeightStretch(YGNodeRef node);

#pragma endregion Height

#pragma region MinHeight

YOGA_API void setNodeMinHeight(YGNodeRef node, float height);
YOGA_API void setNodeMinHeightPercent(YGNodeRef node, float height);
YOGA_API void setNodeMinHeightMaxContent(YGNodeRef node);
YOGA_API void setNodeMinHeightFitContent(YGNodeRef node);
YOGA_API void setNodeMinHeightStretch(YGNodeRef node);

#pragma endregion MinHeight

#pragma region MaxHeight

YOGA_API void setNodeMaxHeight(YGNodeRef node, float height);
YOGA_API void setNodeMaxHeightPercent(YGNodeRef node, float height);
YOGA_API void setNodeMaxHeightMaxContent(YGNodeRef node);
YOGA_API void setNodeMaxHeightFitContent(YGNodeRef node);
YOGA_API void setNodeMaxHeightStretch(YGNodeRef node);

#pragma endregion MaxHeight

#pragma region FlexBasis

YOGA_API void setNodeFlexBasis(YGNodeRef node, float width);
YOGA_API void setNodeFlexBasisPercent(YGNodeRef node, float width);
YOGA_API void setNodeFlexBasisAuto(YGNodeRef node);
YOGA_API void setNodeFlexBasisMaxContent(YGNodeRef node);
YOGA_API void setNodeFlexBasisFitContent(YGNodeRef node);
YOGA_API void setNodeFlexBasisStretch(YGNodeRef node);

#pragma endregion FlexBasis

#pragma region Padding & Gap

YOGA_API void setNodePadding(YGNodeRef node, YGEdge edge, float padding);
YOGA_API void setNodePaddingPercent(YGNodeRef node, YGEdge edge, float padding);
YOGA_API void setNodeGap(YGNodeRef node, YGGutter gutter, float gapLength);
YOGA_API void setNodeGapPercent(YGNodeRef node, YGGutter gutter, float gapLength);

#pragma endregion Padding & Gap

#pragma region Style

YOGA_API void copyNodeStyle(YGNodeRef dstNode, YGNodeRef srcNode);
YOGA_API void setNodeDirection(YGNodeRef node, int direction);
YOGA_API void setNodeFlexDirection(YGNodeRef node, int flexDirection);
YOGA_API void setNodeJustifyContent(YGNodeRef node, int justifyContent);
YOGA_API void setNodeAlignContent(YGNodeRef node, int alignContent);
YOGA_API void setNodeAlignItems(YGNodeRef node, int alignItems);
YOGA_API void setNodeAlignSelf(YGNodeRef node, int alignSelf);
YOGA_API void setNodePositionType(YGNodeRef node, int positionType);
YOGA_API void setNodeFlexWrap(YGNodeRef node, int flexWrap);
YOGA_API void setNodeOverflow(YGNodeRef node, int overflow);
YOGA_API void setNodeDisplay(YGNodeRef node, int display);
YOGA_API void setNodeFlex(YGNodeRef node, float flex);
YOGA_API void setNodeFlexGrow(YGNodeRef node, float flexGrow);
YOGA_API void setNodeFlexShrink(YGNodeRef node, float flexShrink);

#pragma endregion Style

YOGA_API float getNodeLeft(YGNodeRef node);
YOGA_API float getNodeTop(YGNodeRef node);
YOGA_API float getNodeWidth(YGNodeRef node);
YOGA_API float getNodeHeight(YGNodeRef node);
}
