using UnityEngine;

namespace Feko.UniFlexBox
{
    public class UniFlexBoxItem
    {
        [field: SerializeField]
        public int Order { get; set; }
        [field: SerializeField]
        public Flex Flex { get; set; }
        [field: SerializeField]
        public float FlexGrow { get; set; }
        [field: SerializeField]
        public float FlexShrink { get; set; }
        [field: SerializeField]
        public float FlexBasis { get; set; }
        [field: SerializeField]
        public AlignSelf AlignSelf { get; set; }
    }
}

