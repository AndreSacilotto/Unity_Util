using System.Reflection;
using UnityEditor;
using UnityEngine;
using Spectra.Util.Editor;

namespace Spectra.Attributes
{
    [CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
    public class InspectorButtonAttributeDrawer : PropertyDrawer
    {
        int len = 0;
        bool draw = false;

        MethodInfo[] methodsInfo = null;

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            if (!draw)
                return base.GetPropertyHeight(prop, label);
            return base.GetPropertyHeight(prop, label) + (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * len;
        }

        public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
        {
            Rect rect = new Rect(pos.x, pos.y, pos.width, base.GetPropertyHeight(prop, label));
            UtilEditorGUI.DrawDefaultPropretyDrawer(rect, prop, label);

            var atr = attribute as InspectorButtonAttribute;
            var obj = prop.serializedObject.targetObject;

            if (methodsInfo == null)
                methodsInfo = obj.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            draw = EditorApplication.isPlaying ?
                atr.execute.HasFlag(InspectorButtonAttribute.Execute.PlayMode) :
                atr.execute.HasFlag(InspectorButtonAttribute.Execute.EditMode);

            if (methodsInfo == null || methodsInfo.Length < 0)
                EditorGUI.HelpBox(pos, "Method(s) not found", MessageType.Error);
            else if (draw)
            {
                len = atr.methodNames.Length;
                var methods = new MethodInfo[len];
                for (int i = 0, e = 0; e < len; i++)
                {
                    if (i >= methodsInfo.Length)
                    {
                        Debug.LogError($"Cant find Func named {atr.methodNames[e]}");
                        return;
                    }
                    else if (methodsInfo[i].Name == atr.methodNames[e])
                    {
                        methods[e++] = methodsInfo[i];
                        i = -1;
                    }
                }
                foreach (var x in methods)
                {
                    rect = new Rect(pos.x, rect.y + rect.height + EditorGUIUtility.standardVerticalSpacing, pos.width, EditorGUIUtility.singleLineHeight);
                    if (GUI.Button(rect, ObjectNames.NicifyVariableName(x.Name)))
                        x.Invoke(obj, atr.objParam);
                }
            }
        }
    }
}
