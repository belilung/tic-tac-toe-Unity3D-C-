using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DummyInEditor))]
public class CustomEditorr : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DummyInEditor myScript = (DummyInEditor)target;
        if (GUILayout.Button("Change Colliders For Teleport"))
        {
            myScript.AddMeshColliders();
        }
    }
}