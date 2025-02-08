using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Feko.UniFlexBox.Samples
{
    public class SampleCarousel : MonoBehaviour
    {
        [SerializeField]
        private Button _previous;

        [SerializeField]
        private Button _next;

        private BaseSample[] _samples;
        private int _currentSample = 0;

        private void Start()
        {
            _samples = GetComponentsInChildren<BaseSample>();
            for (var index = 0; index < _samples.Length; index++)
            {
                BaseSample baseSample = _samples[index];
                bool isFirstSample = index == 0;
                baseSample.gameObject.SetActive(isFirstSample);
                if (isFirstSample)
                {
                    baseSample.StartDemo();
                }
            }

            _previous.onClick.AddListener(() => ChangeSample(-1));
            _next.onClick.AddListener(() => ChangeSample(1));
        }

        private void ChangeSample(int indexOffset)
        {
            int nextSampleIndex = (_currentSample + indexOffset + _samples.Length) % _samples.Length;
            _samples[_currentSample].gameObject.SetActive(false);
            BaseSample nextSample = _samples[nextSampleIndex];
            nextSample.gameObject.SetActive(true);
            StartCoroutine(ChangeSampleCoroutine(nextSample));
            _currentSample = nextSampleIndex;
        }

        private IEnumerator ChangeSampleCoroutine(BaseSample nextSample)
        {
            yield return null;
            nextSample.StartDemo();
        }
    }
}