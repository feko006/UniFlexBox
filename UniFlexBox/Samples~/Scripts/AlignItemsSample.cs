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
            SetValue(YGAlign.FlexStart);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGAlign value)
        {
            SetElapsedTime(0f);
            _layoutGroup.AlignItems = value;
            _text.text = $"Align Items: {value.ToString()}";
        }

        private YGAlign GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGAlign)) as YGAlign[];
            return values[((int)_layoutGroup.AlignItems + 1) % values.Length];
        }
    }
}