using UnityEngine;

namespace Spectra.Others
{
    public class FPSCounterComplex : Singleton.MonoBehaviourSingleton<FPSCounterComplex>
    {
        [SerializeField]
        private int sampleSize = 10;

        [SerializeField]
        private float measurePeriod = 0.5f;
        private float nextPeriod;


        private int[] samples;
        int sampleIndex = 0;


        private int current;
        private int average;
        private int min;
        private int max;

        public event System.Action<string, string, string, string> OnFPSMeasure;

        public int CurrentFps => current;
        public int AverageFps => average;
        public int MinFps => min;
        public int MaxFps => max;

        void Start()
        {
            samples = new int[sampleSize];
            min = 10000;
            max = 0;
            GetNextPeriod();
        }

        void LateUpdate()
        {
            if (Time.realtimeSinceStartup > nextPeriod)
            {
                current = (int)(1f / Time.unscaledDeltaTime);
                GetNextPeriod();

                samples[sampleIndex++] = current;
                if (sampleIndex >= sampleSize)
                    sampleIndex = 0;
                
                int sum = 0;
                for (int i = 0; i < sampleSize; i++)
                    sum += samples[i];
                average = sum / sampleSize;

                if (current < min)
                    min = current;
                else if (current > max)
                    max = current;

                OnFPSMeasure?.Invoke(current.ToString(), average.ToString(), min.ToString(), max.ToString());
            }
        }

        void GetNextPeriod() => nextPeriod = Time.realtimeSinceStartup + measurePeriod;
    }
}