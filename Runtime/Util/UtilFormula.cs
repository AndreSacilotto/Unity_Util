public static class UtilFormula
{
    /// <param name="valueWorth">Value Between 0 - ~1 (never more or equal 1). Bigger this value is less the value worth</param>
    /// <param name="reduction">Value Between 0 - ~100 (never more or equal 100)</param>
    public static float DiminishingReturns(float value, float valueWorth, float reduction = 0) =>
        100 / (100 + value * (1 - valueWorth) - reduction);

    public static float ValuePercent(float value, float max) => value / max;
    public static float ValuePercent(float value, float min, float max) => (value - min) / (max - min);

}
