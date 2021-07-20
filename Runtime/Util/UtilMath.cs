using System;

namespace Spectra.Util
{
    public static class UtilMath
    {
        public static float ValuePlusPercent(float value, float percent) => value * (1 + percent);
        public static float ValueLessPercent(float value, float percent) => value * (1 - percent);

        public static float CorrectPrecision(float value) => (float)Math.Round(value, 6);
        public static int BoolValue10(this bool value) => value ? 1 : 0;
        public static int BoolValue11(this bool value) => value ? 1 : -1;

        public static int IntPow(this int b, uint e)
        {
            if (e == 0) return 1;
            else if (e == 1) return b;

            int ret = 1;
            while (e != 0)
            {
                if ((e & 1) == 1)
                    ret *= b;
                b *= b;
                e >>= 1;
            }
            return ret;
        }
    }
}
