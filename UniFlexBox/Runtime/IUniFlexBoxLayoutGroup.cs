using System.Collections.Generic;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutGroup : IUniFlexBoxLayoutElement
    {
        /// <summary>
        /// Make sure to call <see cref="NotifyPaddingConstraintsChanged"/> after modifying the constraints list.
        /// </summary>
        List<PaddingConstraint> PaddingConstraints { get; set; }
        
        void NotifyPaddingConstraintsChanged();

        /// <summary>
        /// Make sure to call <see cref="NotifyGapConstraintsChanged"/> after modifying the constraints list.
        /// </summary>
        List<GapConstraint> GapConstraints { get; set; }
        
        void NotifyGapConstraintsChanged();
    }
}