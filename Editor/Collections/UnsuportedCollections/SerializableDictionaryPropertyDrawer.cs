using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Spectra.Collections
{
    [CustomPropertyDrawer(typeof(SerializableDictionary<,>), false)]
    public class SerializableDictionaryPropertyDrawer : PropertyDrawer
    {
        const string KEYS = "keys";
        const string VALUES = "values";
        const string COLLISION = "collision";

        ReorderableList reorderableList = null;
        SerializedProperty[] myList = null;
        bool isFold = true;

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            var collision = prop.FindPropertyRelative(COLLISION);

            var vertSpace = EditorGUIUtility.standardVerticalSpacing;

            var height = EditorGUIUtility.singleLineHeight + vertSpace;

            if (collision.boolValue)
                height += EditorGUIUtility.singleLineHeight * 1.5f + vertSpace;

            if (myList != null && isFold)
                height += reorderableList.GetHeight() + vertSpace;

            return height;
        }

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight;
            var vertSpace = EditorGUIUtility.standardVerticalSpacing;
            var labelWidth = EditorGUIUtility.labelWidth;

            var keys = prop.FindPropertyRelative(KEYS);
            var values = prop.FindPropertyRelative(VALUES);
            var collision = prop.FindPropertyRelative(COLLISION);

            if (reorderableList == null)
            {
                myList = new SerializedProperty[2] { keys, values };
                reorderableList = new ReorderableList(prop.serializedObject, myList[0], true, true, true, true)
                {
                    drawElementCallback = DrawElementCallback,
                    onAddCallback = OnAddCallback,
                    onRemoveCallback = OnRemoveCallback,
                    onReorderCallbackWithDetails = OnReorderCallbackWithDetails,
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
            var listSize = EditorGUI.IntField(rect, keys.arraySize);
            if (EditorGUI.EndChangeCheck())
            {
                keys.arraySize = listSize;
                values.arraySize = listSize;
            }

            if (collision.boolValue)
            {
                rect = new Rect(pos.x, rect.y + rect.height + vertSpace, pos.width, lineHeight * 1.5f);
                EditorGUI.HelpBox(rect, "Key collision", MessageType.Warning);
            }

            if (isFold)
            {
                rect = new Rect(pos.x, rect.y + rect.height + vertSpace, pos.width, pos.height);
                reorderableList?.DoList(rect);
            }
        }

        private void OnReorderCallbackWithDetails(ReorderableList list, int oldIndex, int newIndex)
        {
            myList[1].MoveArrayElement(oldIndex, newIndex);
        }

        private void OnRemoveCallback(ReorderableList list)
        {
            myList[0].DeleteArrayElementAtIndex(list.index);
            myList[1].DeleteArrayElementAtIndex(list.index);
        }

        private void OnAddCallback(ReorderableList list)
        {
            //myList[0].InsertArrayElementAtIndex(list.index);
            //myList[1].InsertArrayElementAtIndex(list.index);
            myList[0].arraySize++;
            myList[1].arraySize++;
            list.index = list.serializedProperty.arraySize - 1;
        }

        private void DrawElementCallback(Rect pos, int index, bool isActive, bool isFocused)
        {
            var labelWidth = EditorGUIUtility.labelWidth;
            var padding = ReorderableList.Defaults.padding;
            var handleW = ReorderableList.Defaults.dragHandleWidth;

            pos = new Rect(pos.x + padding, pos.y + 1, pos.width - padding, pos.height);

            Rect rect;

            EditorGUIUtility.labelWidth = labelWidth - handleW - padding;

            pos = EditorGUI.PrefixLabel(pos, new GUIContent("Element " + index));

            EditorGUIUtility.labelWidth = 42f;
            rect = new Rect(pos.x, pos.y, pos.width / 2f - 5f, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(rect, myList[0].GetArrayElementAtIndex(index), new GUIContent("Key"));

            rect.x = pos.x + rect.width + 10f;
            EditorGUI.PropertyField(rect, myList[1].GetArrayElementAtIndex(index), new GUIContent("Value"));

            EditorGUIUtility.labelWidth = labelWidth;
        }

    }
}