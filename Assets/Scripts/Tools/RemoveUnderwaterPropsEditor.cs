using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveUnderwaterProps))]
public class RemoveUnderwaterTreesProps: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RemoveUnderwaterProps underwaterTreesScript = (RemoveUnderwaterProps)target;

        if (GUILayout.Button("Remove Underwater Trees"))
        {
            underwaterTreesScript.RemoveTreesUnderWater();
        }
    }
}