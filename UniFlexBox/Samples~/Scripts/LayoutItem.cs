using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feko.UniFlexBox.Samples
{
    public class LayoutItem : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Graphic _graphic;

        public void SetUp(int number, Color color)
        {
            _text.text = number.ToString();
            _graphic.color = color;
        }
    }
}