using SysRandom = System.Random;
using UnityRandom = UnityEngine.Random;

namespace Spectra.Util
{
    public static class UtilRandom
    {
        #region Unity.Random

        public static float Random01() => UnityRandom.value;
        public static int Random01Int() => UnityRandom.Range(0, 100);
        public static int PercentChanceInt() => UnityRandom.Range(1, 101);


        static UnityRandom.State state;
        public static void StoreState(int newSeed = -1)
        {
            state = UnityRandom.state;
            if (newSeed == -1)
                newSeed = System.Environment.TickCount;
            UnityRandom.InitState(newSeed);
        }
        public static void RestoreState() => UnityRandom.state = state;

        #endregion

        #region System.Random

        public static double NextDouble(this SysRandom random, double minInclusive, double maxExclusive) => random.NextDouble() * (maxExclusive - minInclusive) + minInclusive;
        public static double NextDouble(this SysRandom random, double maxExclusive) => random.NextDouble() * maxExclusive;

        public static float NextFloat(this SysRandom random, float minInclusive, float maxExclusive) => ((float)random.NextDouble()) * (maxExclusive - minInclusive) + minInclusive;
        public static float NextFloat(this SysRandom random, float maxExclusive) => ((float)random.NextDouble()) * maxExclusive;

        #endregion



    }
}
