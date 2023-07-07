using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace RhythmTools.Editor
{

    [CustomEditor(typeof(Conductor))]
    public class ConductorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            Conductor myConductor = (Conductor)target;
            EditorGUILayout.IntField("Beat Count", myConductor.beat);
        }
    }
}