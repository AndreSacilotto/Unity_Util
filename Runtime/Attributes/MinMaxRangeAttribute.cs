using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class MinMaxRangeAttribute : PropertyAttribute
{
    public float min;
    public float max;

    public MinMaxRangeAttribute(float min = 0, float max = 1)
    {
        this.min = min;
        this.max = max;
    }
}