using System;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [Serializable]
    public struct PaddingConstraint
    {
        public YGEdge Edge;

        [SerializeField]
        private ConstraintUnit _unit;

        public ConstraintUnit Unit
        {
            get => _unit;
            set
            {
                if (!EnsureValidValues(value))
                {
                    _unit = value;
                }
            }
        }

        public float Value;

        private bool EnsureValidValues(ConstraintUnit unit)
        {
            if (unit != ConstraintUnit.Points && unit != ConstraintUnit.Percent)
            {
                _unit = ConstraintUnit.Points;
                return true;
            }

            return false;
        }
    }
}