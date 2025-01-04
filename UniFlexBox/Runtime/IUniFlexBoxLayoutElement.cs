using System.Collections.Generic;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutElement : ILayoutElement
    {
        List<DimensionConstraint> DimensionConstraints { get; set; }
        void AddConstraint(DimensionConstraint constraint);
        void RemoveConstraint(DimensionConstraint constraint);
    }
}