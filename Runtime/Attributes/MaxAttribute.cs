using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class MaxAttribute : PropertyAttribute
{
    public readonly float max;

    public MaxAttribute(float max)
    {
        this.max = max;
    }
}