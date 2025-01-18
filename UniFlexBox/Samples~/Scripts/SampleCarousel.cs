using System.Linq;
using UnityEngine;

namespace Feko.UniFlexBox.Samples
{
    public class SampleCarousel : MonoBehaviour
    {
        private BaseSample[] _samples;

        private void Start()
        {
            _samples = GetComponentsInChildren<BaseSample>();
            _samples.First().StartDemo();
        }
    }
}