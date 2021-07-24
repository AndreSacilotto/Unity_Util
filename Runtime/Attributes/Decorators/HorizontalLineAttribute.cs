using System;
using UnityEngine;

namespace Spectra.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class HorizontalLineAttribute : PropertyAttribute
    {
        public readonly float spaceHeight;
        public readonly float lineHeight;
        public readonly Color lineColor;

        public HorizontalLineAttribute(float lineHeight, float spaceHeight = 0)
        {
            this.spaceHeight = spaceHeight;
            this.lineHeight = lineHeight;

            lineColor = Color.gray;
        }

        public HorizontalLineAttribute(float lineHeight, float spaceHeight, float r, float g, float b)
        {
            this.spaceHeight = spaceHeight;
            this.lineHeight = lineHeight;

            lineColor = new Color(r, g, b);
        }
    }
}