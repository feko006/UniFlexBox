using System.Collections.Generic;
using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutElement : ILayoutElement
    {
        YGAlign AlignSelf { get; set; }
        float Flex { get; set; }
        float FlexGrow { get; set; }
        float FlexShrink { get; set; }

        /// <summary>
        /// If you add elements to this list, make sure to re-assign it so that the changes are propagated properly.
        /// Prefer to use the AddConstraint and RemoveConstraint methods.
        /// </summary>
        List<DimensionConstraint> DimensionConstraints { get; set; }

        void AddConstraint(DimensionConstraint constraint);
        void RemoveConstraint(DimensionConstraint constraint);
        void RemoveConstraint(int index);
    }
}