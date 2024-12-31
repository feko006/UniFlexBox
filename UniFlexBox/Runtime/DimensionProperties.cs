using System;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct DimensionProperties
    {
        public bool Use;
        public bool Auto;
        public bool Stretch;
        public bool FitContent;
        public bool MaxContent;
        public float Size;
        public float Percent;
    }
}