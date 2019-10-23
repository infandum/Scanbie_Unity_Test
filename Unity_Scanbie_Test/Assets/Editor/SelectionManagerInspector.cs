using Assets.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    [CustomEditor(typeof(SelectionManager))]
    public class SelectionManagerInspector : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SelectionManager selectionManager = (SelectionManager) target;
            DrawDefaultInspector();

            selectionManager.FileName = EditorGUILayout.TextField("File name: ", selectionManager.FileName);

            SerializedProperty objects = serializedObject.FindProperty("_editableObjects");
            if (objects.arraySize <= 0)
            if (GUILayout.Button("Get Editable Object"))
            {
                selectionManager.GetAllEditableObjects();
            }

            //Uncomment in you want to be able too edit the list
            //EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(objects, true);
            //if (EditorGUI.EndChangeCheck())
            //{
            //serializedObject.ApplyModifiedProperties();
            //}

            if (GUILayout.Button("Save to file"))
            {
                selectionManager.SaveObjectsToFile();
            }

            if (GUILayout.Button("Load from file"))
            {
                selectionManager.LoadObjectsFromFile();
            }

        }
           
    }   
}
