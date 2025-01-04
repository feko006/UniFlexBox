using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutElement : ILayoutElement
    {
        DimensionConstraint[] DimensionConstraints { get; set; }
    }
}