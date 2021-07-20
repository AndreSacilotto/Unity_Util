using System;
using System.Collections.Generic;
using System.Reflection;

namespace Spectra.Util
{
    public class UtilReflection
    {
        public static BindingFlags GetBindingFlags(bool isPublic, bool isInternalProtectInternal, bool isFromInstance, bool isStatic = false, bool isUpHierarchy = false, bool isDeclaredOnClass = false)
        {
            BindingFlags bf = BindingFlags.Default;
            if (isPublic)
                bf |= BindingFlags.Public;
            if (isInternalProtectInternal)
                bf |= BindingFlags.NonPublic;
            if (isFromInstance)
                bf |= BindingFlags.Instance;
            if (isStatic)
                bf |= BindingFlags.Static;
            if (isUpHierarchy)
                bf |= BindingFlags.FlattenHierarchy;
            if (isDeclaredOnClass)
                bf |= BindingFlags.DeclaredOnly;
            return bf;
        }

        public static List<PropertyInfo> GetProperties(Type type, bool get = true, bool set = true, BindingFlags binding = (BindingFlags)52)
        {
            if (!get && !set)
                return null;
            var list = new List<PropertyInfo>();
            var props = type.GetProperties(binding);

            foreach (var x in props)
                if ((get && set && x.CanRead && x.CanWrite) || (get && !set && x.CanRead) || (!get && set && x.CanWrite))
                    list.Add(x);
            return list;
        }

        public static void SetInternalStaticField(Type fieldClass, string fieldName, object value, out FieldInfo field)
        {
            field = fieldClass.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            field.SetValue(null, value);
        }

        public static T GetInternalStaticField<T>(Type fieldClass, string fieldName, out FieldInfo field)
        {
            field = fieldClass.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            return (T)field.GetValue(null);
        }

        public static T InvokeInternalStaticMethod<T>(Type methodClass, string methodName, out MethodInfo method, params object[] methodParam)
        {
            method = methodClass.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            return (T)method.Invoke(null, methodParam);
        }

    }

}