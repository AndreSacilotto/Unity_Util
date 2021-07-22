using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute), false)]
public class MinMaxRangeAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        var content = EditorGUI.BeginProperty(pos, label, property);
        pos = EditorGUI.PrefixLabel(pos, content);

        var atr = attribute as MinMaxRangeAttribute;
        var fieldSize = pos.width / 5;
        var margin = pos.width * .02f;

        LeftMiddleRight(ref pos, fieldSize, fieldSize, margin, out var l, out var m, out var r);

        if (property.propertyType == SerializedPropertyType.Vector2)
            CreateVector2(property, atr.min, atr.max, l, m, r);
        else if (property.propertyType == SerializedPropertyType.Vector2Int)
            CreateVector2Int(property, atr.min, atr.max, l, m, r);
        else
        {
            base.OnGUI(pos, property, label);
            Debug.LogError("The Attribute MinMaxRange can only be used with Vector2 and Vector2Int");
        }

        EditorGUI.EndProperty();
    }

    void CreateVector2(SerializedProperty property, float rangeMin, float rangeMax, Rect l, Rect m, Rect r)
    {
        float minValue = property.vector2Value.x;
        float maxValue = property.vector2Value.y;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(m, ref minValue, ref maxValue, rangeMin, rangeMax);
        minValue = EditorGUI.FloatField(l, minValue);
        maxValue = EditorGUI.FloatField(r, maxValue);
        if (EditorGUI.EndChangeCheck())
            property.vector2Value = new Vector2(minValue, maxValue);
    }

    void CreateVector2Int(SerializedProperty property, float rangeMin, float rangeMax, Rect l, Rect m, Rect r)
    {
        float minValue = property.vector2IntValue.x;
        float maxValue = property.vector2IntValue.y;

        EditorGUI.BeginChangeCheck();
        EditorGUI.MinMaxSlider(m, ref minValue, ref maxValue, rangeMin, rangeMax);
        int min = EditorGUI.IntField(l, Mathf.RoundToInt(minValue));
        int max = EditorGUI.IntField(r, Mathf.RoundToInt(maxValue));
        if (EditorGUI.EndChangeCheck())
            property.vector2IntValue = new Vector2Int(min, max);
    }

    void LeftMiddleRight(ref Rect pos, float left, float right, float margin, out Rect rectLeft, out Rect rectMiddle, out Rect rectRight)
    {
        rectLeft = new Rect(pos.x, pos.y, left, pos.height);
        rectMiddle = new Rect(rectLeft.xMax + margin, pos.y, pos.width - left - right - margin * 2, pos.height);
        rectRight = new Rect(rectMiddle.xMax + margin, pos.y, right, pos.height);
    }

}
