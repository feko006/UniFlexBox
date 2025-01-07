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
        float AspectRatio { get; set; }

        /// <summary>
        /// Make sure to call <see cref="NotifyDimensionConstraintsChanged"/> after modifying the constraints list.
        /// </summary>
        List<DimensionConstraint> DimensionConstraints { get; set; }

        void NotifyDimensionConstraintsChanged();
    }
}