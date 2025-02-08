using System;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct DimensionConstraint
    {
        [SerializeField]
        private ConstraintType _type;

        public ConstraintType Type
        {
            get => _type;
            set
            {
                EnsureValidValues(value, Unit);
                _type = value;
            }
        }

        [SerializeField]
        private ConstraintUnit _unit;

        public ConstraintUnit Unit
        {
            get => _unit;
            set
            {
                if (!EnsureValidValues(Type, value))
                {
                    _unit = value;
                }
            }
        }

        public float Value;

        private bool EnsureValidValues(ConstraintType type, ConstraintUnit unit)
        {
            if (type != ConstraintType.ExactHeight
                && type != ConstraintType.ExactWidth
                && unit == ConstraintUnit.Auto)
            {
                Unit = ConstraintUnit.Points;
                return true;
            }

            return false;
        }
    }
}