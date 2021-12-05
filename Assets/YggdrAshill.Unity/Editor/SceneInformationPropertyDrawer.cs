using UnityEditor;
using UnityEngine;

namespace YggdrAshill.Unity
{
    [CustomPropertyDrawer(typeof(SceneInformation))]
    internal class SceneInformationPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            var scene = property.FindPropertyRelative("scene");
            EditorGUI.PropertyField(position, scene, null);

            position.y += position.height;

            using (new EditorGUI.DisabledGroupScope(true))
            {
                var path = property.FindPropertyRelative("path");
                EditorGUI.PropertyField(position, path, null);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property) * 2;
        }
    }
}
