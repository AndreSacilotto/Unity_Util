using UnityEngine;

namespace Spectra.Others
{
    public class FPSCounter : Singleton.MonoBehaviourSingleton<FPSCounter>
    {
        const float fpsMeasurePeriod = 0.5f;

        int fpsAccumulator = 0;
        float fpsNextPeriod = 0;

        [SerializeField] public int currentFps;

        public event System.Action<string> OnFPSMeasure;

        void Start()
        {
            fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        }

        void Update()
        {
            fpsAccumulator++;
            if (Time.realtimeSinceStartup > fpsNextPeriod)
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
                OnFPSMeasure?.Invoke(currentFps.ToString("0.##"));
            }
        }
    }
}