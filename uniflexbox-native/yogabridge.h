#pragma once

#ifndef YOGA_API
#define YOGA_API __declspec(dllexport)
#endif

#include "yoga/Yoga.h"

extern "C" YOGA_API YGNodeRef getNewNode();
extern "C" YOGA_API float getNodeWidth(YGNodeRef node);