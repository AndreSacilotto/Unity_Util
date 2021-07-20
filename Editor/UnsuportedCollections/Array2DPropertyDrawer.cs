using UnityEngine;
using UnityEditor;

namespace Spectra.Unsuported.Collections
{
    [CustomPropertyDrawer(typeof(Array2D<>), false)]
    public class Array2DPropertyDrawer : PropertyDrawer
    {
        const string ARR = "internalArray";
        const string ROWS = "rows";
        const string COLS = "columns";

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            var height = base.GetPropertyHeight(prop, label);
            var rows = prop.FindPropertyRelative(ROWS).intValue;
            if (isFold && rows > 0)
                height += rows * (EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight) + 4;
            return height;
        }

        bool isFold;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight;
            var fieldWidth = EditorGUIUtility.fieldWidth;
            var labelWidth = EditorGUIUtility.labelWidth;

            var arr = prop.FindPropertyRelative(ARR);
            var rows = prop.FindPropertyRelative(ROWS);
            var columns = prop.FindPropertyRelative(COLS);

            Rect rect;

            rect = new Rect(pos.x, pos.y, labelWidth, lineHeight);
            isFold = EditorGUI.Foldout(rect, isFold, label, true);

            EditorGUI.BeginChangeCheck();
            {
                var lblSize = EditorStyles.label.CalcSize(new GUIContent(rows.displayName)).x;
                EditorGUIUtility.labelWidth = lblSize;

                rect = new Rect(pos.width - fieldWidth - lblSize, pos.y, lblSize + fieldWidth, lineHeight);
                EditorGUI.PropertyField(rect, rows);

                lblSize = EditorStyles.label.CalcSize(new GUIContent(columns.displayName)).x;
                EditorGUIUtility.labelWidth = lblSize;

                rect = new Rect(rect.x - fieldWidth - lblSize * 1.2f, pos.y, lblSize + fieldWidth, lineHeight);
                EditorGUI.PropertyField(rect, columns);
            }
            if (EditorGUI.EndChangeCheck())
                arr.arraySize = columns.intValue * rows.intValue;

            if (isFold)
            {
                EditorGUI.indentLevel++;
                rect = new Rect(pos.x, pos.y + lineHeight + 4, pos.width, lineHeight);
                EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(new GUIContent("00:00")).x;
                var w = pos.width / columns.intValue;
                for (int x = 0; x < columns.intValue; x++)
                {
                    Rect tempRect = new Rect(pos.x + w * x, rect.y, w, rect.height);
                    for (int y = 0; y < rows.intValue; y++)
                    {
                        tempRect.y = rect.y + lineHeight * y + EditorGUIUtility.standardVerticalSpacing;
                        var el = arr.GetArrayElementAtIndex(x * rows.intValue + y);
                        EditorGUI.PropertyField(tempRect, el, new GUIContent($"{x}:{y}"));
                    }
                }
                EditorGUI.indentLevel--;
            }

            EditorGUIUtility.labelWidth = labelWidth;
        }
    }
}