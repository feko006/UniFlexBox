using UnityEngine.UI;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutElement : ILayoutElement
    {
        DimensionProperties MinimumWidth { get; set; }
        DimensionProperties ExactWidth { get; set; }
        DimensionProperties MaximumWidth { get; set; }
        DimensionProperties MinimumHeight { get; set; }
        DimensionProperties ExactHeight { get; set; }
        DimensionProperties MaximumHeight { get; set; }
    }
}