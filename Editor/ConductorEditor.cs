using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace RhythmTools
{

    [CustomEditor(typeof(Conductor))]
    public class ConductorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Conductor myConductor = (Conductor)target;
            EditorGUILayout.IntField("Beat Count", myConductor.beat);
        }
    }
}