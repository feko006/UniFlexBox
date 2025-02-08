using System;
using TMPro;
using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class AlignItemsSample : BaseSample
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private UniFlexBoxLayoutGroup _layoutGroup;

        public override void StartDemo()
        {
            SetValue(YGAlignItems.FlexStart);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGAlignItems value)
        {
            SetElapsedTime(0f);
            _layoutGroup.AlignItems = value;
            _text.text = $"Align Items: {value.ToString()}";
        }

        private YGAlignItems GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGAlignItems)) as YGAlignItems[];
            int index = Array.IndexOf(values, _layoutGroup.AlignItems);
            return values[(index + 1) % values.Length];
        }
    }
}