using System;
using UnityEngine;

namespace Spectra.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}