using System;
using TMPro;
using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class JustifyContentSample : BaseSample
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private UniFlexBoxLayoutGroup _layoutGroup;

        public override void StartDemo()
        {
            SetValue(YGJustify.FlexStart);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGJustify value)
        {
            SetElapsedTime(0f);
            _layoutGroup.JustifyContent = value;
            _text.text = $"Justify Content: {value.ToString()}";
        }

        private YGJustify GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGJustify)) as YGJustify[];
            int index = Array.IndexOf(values, _layoutGroup.JustifyContent);
            return values[(index + 1) % values.Length];
        }
    }
}