using System;
using UnityEngine;

namespace Spectra.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class MenuContextAttribute : PropertyAttribute
    {
        public readonly string contextName;
        public readonly string[] methodsNames;

        public MenuContextAttribute(string contextName, params string[] methodsNames)
        {
            this.contextName = contextName;
            this.methodsNames = methodsNames;
        }
    }
}
