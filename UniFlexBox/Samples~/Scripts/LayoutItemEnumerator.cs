using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class LayoutItemEnumerator : MonoBehaviour
    {
        private readonly Color[] _colors = new Color[3];

        private void Start()
        {
            ColorUtility.TryParseHtmlString("#4bb847", out _colors[0]);
            ColorUtility.TryParseHtmlString("#474bb8", out _colors[1]);
            ColorUtility.TryParseHtmlString("#b8474b", out _colors[2]);
            LayoutItem[] layoutItems = GetComponentsInChildren<LayoutItem>();
            for (var i = 0; i < layoutItems.Length; i++)
            {
                layoutItems[i].SetUp(i + 1, _colors[i % _colors.Length]);
            }
        }
    }
}