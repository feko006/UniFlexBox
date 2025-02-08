using System;
using TMPro;
using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class AlignContentSample : BaseSample
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private UniFlexBoxLayoutGroup _layoutGroup;

        public override void StartDemo()
        {
            SetValue(YGAlignContent.FlexStart);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGAlignContent value)
        {
            SetElapsedTime(0f);
            _layoutGroup.AlignContent = value;
            _text.text = $"Align Content: {value.ToString()}";
        }

        private YGAlignContent GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGAlignContent)) as YGAlignContent[];
            int index = Array.IndexOf(values, _layoutGroup.AlignContent);
            return values[(index + 1) % values.Length];
        }
    }
}