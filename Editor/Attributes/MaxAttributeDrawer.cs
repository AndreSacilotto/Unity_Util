using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MaxAttribute), false)]
public class MaxAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
    {
        var atr = attribute as MaxAttribute;

        if (prop.propertyType == SerializedPropertyType.Float)
        {
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.FloatField(pos, label, prop.floatValue);
            if (value > atr.max)
                value = atr.max;
            if (EditorGUI.EndChangeCheck())
                prop.floatValue = value;
        }
        else if (prop.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.BeginChangeCheck();
            var value = EditorGUI.IntField(pos, label, prop.intValue);
            if (value > atr.max)
                value = Convert.ToInt32(atr.max);
            if (EditorGUI.EndChangeCheck())
                prop.intValue = value;
        }
        else
        {
            base.OnGUI(pos, prop, label);
            Debug.LogError($"The {nameof(MaxAttribute)} can only be used with int and float");
        }

    }

}
