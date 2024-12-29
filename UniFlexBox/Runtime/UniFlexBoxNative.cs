using System;
using System.Runtime.InteropServices;

namespace Feko.UniFlexBox
{
    public static class UniFlexBoxNative
    {
        [DllImport("uniflexbox-native")]
        public static extern IntPtr createNewNode();

        [DllImport("uniflexbox-native")]
        public static extern void addChild(IntPtr node, IntPtr child);

        [DllImport("uniflexbox-native")]
        public static extern void freeNode(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void removeAllChildren(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void calculateLayout(IntPtr node);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeWidth(IntPtr node, float width);

        [DllImport("uniflexbox-native")]
        public static extern void setNodeHeight(IntPtr node, float height);

        [DllImport("uniflexbox-native")]
        public static extern void setFlexDirection(IntPtr node, int flexDirection);

        [DllImport("uniflexbox-native")]
        public static extern void setAlignItems(IntPtr node, int alignItems);

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