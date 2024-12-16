#include "pch.h"
#include "yogabridge.h"

YGNodeRef getNewNode()
{
	auto node = YGNodeNew();
	YGNodeStyleSetWidth(node, 100.0f);
	return node;
}

float getNodeWidth(YGNodeRef node)
{
	return YGNodeStyleGetWidth(node).value;
}