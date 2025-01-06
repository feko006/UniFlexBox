using System;
using System.Collections.Generic;

namespace Feko.UniFlexBox
{
    public interface IUniFlexBoxLayoutGroup : IUniFlexBoxLayoutElement
    {
        /// <summary>
        /// If you add elements to this list, make sure to re-assign it so that the changes are propagated properly.
        /// Prefer to use the AddPaddingConstraint and RemovePaddingConstraint methods.
        /// </summary>
        List<PaddingConstraint> PaddingConstraints { get; set; }

        /// <summary>
        /// If you add elements to this list, make sure to re-assign it so that the changes are propagated properly.
        /// Prefer to use the AddGapConstraint and RemoveGapConstraint methods.
        /// </summary>
        List<GapConstraint> GapConstraints { get; set; }

        void AddPaddingConstraint(PaddingConstraint constraint);
        void RemovePaddingConstraint(PaddingConstraint constraint);
        void RemovePaddingConstraints(Predicate<PaddingConstraint> predicate);
        void RemovePaddingConstraint(int index);

        void AddGapConstraint(GapConstraint constraint);
        void RemoveGapConstraint(GapConstraint constraint);
        void RemoveGapConstraints(Predicate<GapConstraint> predicate);
        void RemoveGapConstraint(int index);
    }
}