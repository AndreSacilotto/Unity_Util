using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Spectra.Collections
{
    [CustomPropertyDrawer(typeof(SerializableQueue<>), false)]
    [CustomPropertyDrawer(typeof(SerializableStack<>), false)]
    public class SerializableQueueStackPropertyDrawer : PropertyDrawer
    {
        const string LIST = "list";

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            var vertSpace = EditorGUIUtility.standardVerticalSpacing;

            var height = EditorGUIUtility.singleLineHeight + vertSpace;

            if (myList != null && isFold)
                height += reorderableList.GetHeight() + vertSpace;

            return height;
        }

        ReorderableList reorderableList = null;
        SerializedProperty myList = null;
        bool isFold = true;

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight;
            var vertSpace = EditorGUIUtility.standardVerticalSpacing;
            var labelWidth = EditorGUIUtility.labelWidth;

            var list = prop.FindPropertyRelative(LIST);

            if (reorderableList == null)
            {
                myList = list;
                reorderableList = new ReorderableList(prop.serializedObject, list, true, true, true, true)
                {
                    drawElementCallback = DrawElementCallback,
                    headerHeight = 0,
                    elementHeight = lineHeight,
                };
            }

            Rect rect;
            rect = new Rect(pos.x, pos.y, labelWidth, lineHeight);
            isFold = EditorGUI.Foldout(rect, isFold, label, true);

            EditorGUI.BeginChangeCheck();
            var fw = EditorGUIUtility.fieldWidth;
            rect = new Rect(pos.width - fw * 0.63f, pos.y, fw, lineHeight);
            var listSize = EditorGUI.IntField(rect, list.arraySize);
            if (EditorGUI.EndChangeCheck())
                list.arraySize = listSize;

            if (isFold)
                reorderableList.DoList(new Rect(pos.x, rect.y + rect.height + vertSpace, pos.width, pos.height));
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var labelWidth = EditorGUIUtility.labelWidth;

            var leftPadding = ReorderableList.Defaults.padding;
            var handleW = ReorderableList.Defaults.dragHandleWidth;

            EditorGUIUtility.labelWidth = labelWidth - handleW - leftPadding;

            rect = new Rect(rect.x + leftPadding, rect.y + 1, rect.width - leftPadding, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rect, myList.GetArrayElementAtIndex(index));

            EditorGUIUtility.labelWidth = labelWidth;
        }
    }
}
