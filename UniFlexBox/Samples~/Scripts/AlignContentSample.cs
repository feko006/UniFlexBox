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
            SetValue(YGAlign.Auto);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGAlign value)
        {
            SetElapsedTime(0f);
            _layoutGroup.AlignContent = value;
            _text.text = $"Align Content: {value.ToString()}";
        }

        private YGAlign GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGAlign)) as YGAlign[];
            return values[((int)_layoutGroup.AlignContent + 1) % values.Length];
        }
    }
}