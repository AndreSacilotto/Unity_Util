using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Spectra.Util.Editor
{
    public static class UtilEditorGUI
    {
        public static T CastTarget<T>(this SerializedObject so) where T : UnityObject => (T)so.targetObject;
        public static T[] CastTargets<T>(this SerializedObject so) where T : UnityObject => Array.ConvertAll(so.targetObjects, x => (T)x);

        public static float GetAndSetLabelWidth(float tempLabelWidth)
        {
            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = tempLabelWidth;
            return labelWidth;
        }

        public static void DrawDefaultPropretyDrawer(Rect pos, SerializedProperty prop, GUIContent label) =>
            UtilReflection.InvokeInternalStaticMethod<object>(typeof(EditorGUI), "DefaultPropertyField", out _, pos, prop, label);

        #region Parent and Child
        public static T[] GetPropertiesInstancesByPath<T>(SerializedProperty prop)
        {
            if (prop.hasMultipleDifferentValues)
            {
                var targets = prop.serializedObject.targetObjects;
                var path = prop.propertyPath.Split('.');
                return Array.ConvertAll(targets, x => (T)GetPropertyInstanceByPath(x, path));
            }
            return new T[1] { (T)GetPropertyInstanceByPath(prop) };
        }

        public static object GetPropertyInstanceByPath(SerializedProperty prop)
        {
            var paths = prop.propertyPath.Split('.');
            var target = prop.serializedObject.targetObject;
            return GetPropertyInstanceByPath(target, paths);
        }

        public static object GetPropertyInstanceByPath(UnityObject obj, string[] paths)
        {
            object target = obj;
            Type type = target.GetType();
            for (int i = 0; i < paths.Length; i++)
            {
                var field = type.GetField(paths[i]);
                type = field.FieldType;
                target = field.GetValue(target);
            }
            return target;
        }

        #endregion

        #region ObjectField
        public static void ScriptField(MonoBehaviour obj, Type type)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(obj), type, false);
            EditorGUI.EndDisabledGroup();
        }

        public static UnityEngine.Object ScriptableObjectField(GUIContent content, ScriptableObject obj, Type type)
        {
            return EditorGUILayout.ObjectField(content, MonoScript.FromScriptableObject(obj), type, false);
        }
        #endregion

        #region AutoProperty (BackField)

        const string BACKFIELD = "<{0}>k__BackingField";

        public static FieldInfo GetBackfield(Type type, string propName) =>
            type.GetField(string.Format(BACKFIELD, propName), BindingFlags.NonPublic | BindingFlags.Instance);

        public static SerializedProperty FindAutoProperty(this SerializedObject obj, string propName) =>
            obj.FindProperty(string.Format(BACKFIELD, propName));

        public static SerializedProperty FindAutoPropertyRelative(this SerializedProperty prop, string propName) =>
            prop.FindPropertyRelative(string.Format(BACKFIELD, propName));
        #endregion

        #region Serialized Reflection
        public static List<FieldInfo> GetSerializedFields(Type type)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            var fields = type.GetFields(flags);

            var list = new List<FieldInfo>();
            foreach (var x in fields)
            {
                var IsDelegate = x.FieldType.IsAssignableFrom(typeof(MulticastDelegate));
                if (x.IsNotSerialized || x.IsInitOnly || IsDelegate)
                    continue;
                if (x.IsPublic || x.IsDefined(typeof(SerializeField), false))
                    list.Add(x);
            }
            return list;
        }
        #endregion

        #region FieldInfo
        public static object GetPropertyValue(SerializedProperty property, FieldInfo fieldInfo = null)
        {
            if (fieldInfo == null)
                fieldInfo = GetFieldByPath(property);
            return fieldInfo.GetValue(property.serializedObject.targetObject);
        }

        public static void SetPropertyValue(SerializedProperty property, object value, FieldInfo fieldInfo = null)
        {
            if (fieldInfo == null)
                fieldInfo = GetFieldByPath(property);
            fieldInfo.SetValue(property.serializedObject.targetObject, value);
        }

        public static FieldInfo GetFieldByPath(SerializedProperty property)
        {
            FieldInfo fi = null;
            string path = property.propertyPath;
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var parent = property.serializedObject.targetObject.GetType();
            var paths = path.Split('.');
            for (int i = 0; i < paths.Length; i++)
            {
                fi = parent.GetField(paths[i], flags);
                if (fi == null)
                    return null;
                else if (fi.FieldType.IsArray)
                {
                    parent = fi.FieldType.GetElementType();
                    i += 2;
                    continue;
                }
                else if (fi.FieldType.IsGenericType)
                {
                    parent = fi.FieldType.GetGenericArguments()[0];
                    i += 2;
                    continue;
                }
                else
                    parent = fi.FieldType;
            }
            return fi ?? parent.GetField(path, flags);
        }

        #endregion

    }
}