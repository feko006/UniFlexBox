using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class StaticSample : MonoBehaviour, ISample
    {
        [SerializeField]
        private UniFlexBoxLayoutGroup _layoutGroup;

        public void SetActive(bool active)
        {
            _layoutGroup.enabled = false;
            gameObject.SetActive(active);
        }

        public void StartDemo()
        {
            _layoutGroup.enabled = true;
        }
    }
}