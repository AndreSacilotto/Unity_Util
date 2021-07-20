using UnityEditor;
using UnityEngine;

namespace Spectra.Attributes
{
    [CustomPropertyDrawer(typeof(MenuContextAttribute))]
    public class MenuContextAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) => base.GetPropertyHeight(prop, label);

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            UtilEditor.DrawDefaultPropretyDrawer(pos, prop, label);

            Rect rect = pos;
            var atr = attribute as MenuContextAttribute;
            var obj = prop.serializedObject.targetObject;

            Event e = Event.current;
            rect.height = EditorGUIUtility.labelWidth;

            //if (methodsInfo == null)
            //    methodsInfo = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (e.type == EventType.MouseDown && e.button == 1 && rect.Contains(e.mousePosition))
            {
                GenericMenu context = new GenericMenu();
                var contextLabel = new GUIContent(atr.contextName);
                context.AddItem(contextLabel, false, null);
                context.ShowAsContext();
                e.Use();
            }
        }
    }
}
