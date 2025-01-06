using System;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct PaddingConstraint
    {
        public YGEdge Edge;
        public ConstraintUnit Unit;
        public float Value;
    }
}