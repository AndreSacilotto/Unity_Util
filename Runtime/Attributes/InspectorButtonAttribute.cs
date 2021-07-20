using System;
using UnityEngine;

namespace Spectra.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        [Flags]
        public enum Execute
        {
            EditMode = 1,
            PlayMode = 2,
        }

        public Execute execute;
        public string[] methodNames;
        public object[] objParam;

        public InspectorButtonAttribute(object[] objParam, params string[] methodsNames)
        {
            execute = Execute.PlayMode | Execute.EditMode;
            methodNames = methodsNames;
            this.objParam = objParam;
        }
        public InspectorButtonAttribute(string methodName, params object[] objParam) :
            this(objParam, methodName)
        { }

        public InspectorButtonAttribute(Execute execute, object[] objParam, params string[] methodsNames)
        {
            this.execute = execute;
            methodNames = methodsNames;
            this.objParam = objParam;
        }
        public InspectorButtonAttribute(Execute execute, string methodName, params object[] objParam) :
            this(execute, objParam, methodName)
        { }
    }

}