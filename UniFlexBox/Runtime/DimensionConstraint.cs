using System;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct DimensionConstraint
    {
        public ConstraintType Type;
        public ConstraintUnit Unit;
        public float Value;
    }
}