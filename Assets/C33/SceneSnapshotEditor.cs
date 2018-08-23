using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneSnapshot))]
public class SceneSnapshotEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SceneSnapshot sceneSnapshot = (SceneSnapshot)target;
        if(GUILayout.Button("Show/Hide Current Asset"))
        {

        }
        if (GUILayout.Button("Save (" + sceneSnapshot.assets.Length + ") Snapshots"))
        {
            sceneSnapshot.Snapshot();
        }
    }

}
