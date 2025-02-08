using System;
using TMPro;
using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class FlexDirectionSample : BaseSample
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private UniFlexBoxLayoutGroup _layoutGroup;

        public override void StartDemo()
        {
            SetValue(YGFlexDirection.Column);
        }

        protected override void TriggerUpdate()
        {
            SetValue(GetNextValue());
        }

        private void SetValue(YGFlexDirection value)
        {
            SetElapsedTime(0f);
            _layoutGroup.FlexDirection = value;
            _text.text = $"Flex Direction: {value.ToString()}";
        }

        private YGFlexDirection GetNextValue()
        {
            var values = Enum.GetValues(typeof(YGFlexDirection)) as YGFlexDirection[];
            int index = Array.IndexOf(values, _layoutGroup.FlexDirection);
            return values[(index + 1) % values.Length];
        }
    }
}