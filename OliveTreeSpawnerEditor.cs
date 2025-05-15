using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OliveTreeSpawner))]
public class OliveTreeSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OliveTreeSpawner spawner = (OliveTreeSpawner)target;

        if (GUILayout.Button("Spawn Trees"))
        {
            spawner.SpawnTrees();
        }
    }
}
