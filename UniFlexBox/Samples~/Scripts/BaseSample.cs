using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public abstract class BaseSample : MonoBehaviour, ISample
    {
        [SerializeField]
        private RectTransform _progressContainer;

        [SerializeField]
        private RectTransform _progressBar;

        private readonly float _stepDelay = 3f;
        private float _elapsedTime;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public abstract void StartDemo();

        private void Update()
        {
            SetElapsedTime(_elapsedTime + Time.deltaTime);
            if (_elapsedTime >= _stepDelay)
            {
                TriggerUpdate();
            }
        }

        protected void SetElapsedTime(float elapsedTime)
        {
            _elapsedTime = elapsedTime;
            float progressBarSize = _progressContainer.rect.width * _elapsedTime / _stepDelay;
            _progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, progressBarSize);
        }

        protected abstract void TriggerUpdate();
    }
}