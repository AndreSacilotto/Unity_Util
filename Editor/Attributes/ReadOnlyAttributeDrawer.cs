using UnityEditor;
using UnityEngine;

namespace Spectra.Attributes
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.isArray)
            {
                //GUI.enabled = false;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(position, property, label, true);
                EditorGUI.EndDisabledGroup();
                //GUI.enabled = true;
            }
            else
            {

            }
        }
    }
}