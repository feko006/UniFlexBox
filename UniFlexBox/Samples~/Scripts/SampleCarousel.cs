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

        private ISample[] _samples;
        private int _currentSample;

        private void Start()
        {
            _samples = GetComponentsInChildren<ISample>();
            for (var index = 0; index < _samples.Length; index++)
            {
                ISample baseSample = _samples[index];
                bool isFirstSample = index == 0;
                baseSample.SetActive(isFirstSample);
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
            _samples[_currentSample].SetActive(false);
            ISample nextSample = _samples[nextSampleIndex];
            nextSample.SetActive(true);
            StartCoroutine(ChangeSampleCoroutine(nextSample));
            _currentSample = nextSampleIndex;
        }

        private IEnumerator ChangeSampleCoroutine(ISample nextSample)
        {
            yield return null;
            nextSample.StartDemo();
        }
    }
}