using System;
using UnityEngine;

namespace Spectra.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class LineDecoratorAttribute : PropertyAttribute
    {
        public float spaceHeight;
        public float lineHeight;
        public float lineWidth;
        public Color lineColor;

        public LineDecoratorAttribute(float spaceHeight, float lineHeight, float lineWidth, float r = 138, float g = 138, float b = 138)
        {
            this.spaceHeight = spaceHeight;
            this.lineHeight = lineHeight;
            this.lineWidth = lineWidth;

            this.lineColor = new Color(r, g, b);
        }
    }
}