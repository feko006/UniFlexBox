using System;
using UnityEngine.Serialization;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct GapConstraint
    {
        [FormerlySerializedAs("Edge")]
        public YGGutter Gutter;
        public ConstraintUnit Unit;
        public float Value;
    }
}