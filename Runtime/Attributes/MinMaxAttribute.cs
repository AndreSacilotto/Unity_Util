using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class MinMaxAttribute : PropertyAttribute
{
    public readonly float min;
    public readonly float max;

    public MinMaxAttribute(float min, float max)
    {
        this.max = min;
        this.max = max;
    }
}