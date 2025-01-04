using System;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct DimensionConstraint
    {
        public ConstraintType Type;
        public ConstraintUnit Unit;

        [Tooltip("Used only when Unit is Units or Percent")]
        public float Value;
    }
}