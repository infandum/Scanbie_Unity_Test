using UnityEditor;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Editor
{
    [CustomEditor(typeof(MenuObjectControl))]
    public class ObjectInspector : UnityEditor.Editor
    {
        public string[] options = new string[] { ShadowCastingMode.Off.ToString(), ShadowCastingMode.On.ToString(), ShadowCastingMode.TwoSided.ToString(), ShadowCastingMode.ShadowsOnly.ToString() };
        public int index = 0;
        public override void OnInspectorGUI()
        {
            MenuObjectControl menuObjectControl = (MenuObjectControl) target;
            //DrawDefaultInspector();
            index = (int)menuObjectControl.EditableData.ShadowCasting;

            if (GUILayout.Button("Build From Object"))
            {
                menuObjectControl.GetEditableDataFromObject();
            }
            EditorGUILayout.LabelField("GUID: " + menuObjectControl.EditableData.GUID);
            menuObjectControl.EditableData.Name = EditorGUILayout.TextField("Name: ", menuObjectControl.EditableData.Name);
            menuObjectControl.EditableData.InfoFile = EditorGUILayout.TextField("Info-file: ", menuObjectControl.EditableData.InfoFile);

            EditorGUILayout.LabelField("");
            menuObjectControl.EditableData.MainColor.SetColor(EditorGUILayout.ColorField("Main Color:", menuObjectControl.EditableData.MainColor.ToColor()));
            menuObjectControl.EditableData.Metallic = EditorGUILayout.FloatField("Metallic: ", menuObjectControl.EditableData.Metallic);
            EditorGUILayout.LabelField("Cast Shadows:");
            menuObjectControl.EditableData.ShadowCasting = EditorGUILayout.Popup((int)menuObjectControl.EditableData.ShadowCasting, options);
            menuObjectControl.EditableData.ReceiveShadow = EditorGUILayout.Toggle("Receive Shadows: ", menuObjectControl.EditableData.ReceiveShadow);

            
            if (GUILayout.Button("Build To Object"))
            {
                menuObjectControl.SetEditableDataToObject();
            }
        }
    }
}
