using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Spectra.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            //GUI.enabled = false;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(pos, prop, label, true);
            EditorGUI.EndDisabledGroup();
            //GUI.enabled = true;
        }
    }
}