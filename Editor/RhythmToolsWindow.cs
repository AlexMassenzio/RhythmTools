using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Build.Player;

public enum AUDIO_SYSTEM
{
    Unity,
    FMOD,
    Wwise
}

public class RhythmToolsWindow : EditorWindow
{
    private const string CONDUCTOR_PREFAB_PATH = "Packages/com.alexmassenzio.rhythmtools/Runtime/Conductor.prefab";
    public AUDIO_SYSTEM selectedAudioSystem;

    [MenuItem("Window/RhythmTools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<RhythmToolsWindow>("RhythmTools");
    }

    private void OnGUI()
    {


        Dictionary<AUDIO_SYSTEM, string> audioSystemNames = new Dictionary<AUDIO_SYSTEM, string>()
        {
            { AUDIO_SYSTEM.Unity, "RT_USE_UNITY" },
            { AUDIO_SYSTEM.FMOD, "RT_USE_FMOD" },
            { AUDIO_SYSTEM.Wwise, "RT_USE_WWISE" }
        };

        selectedAudioSystem = (AUDIO_SYSTEM)EditorGUILayout.EnumPopup("Audio System", selectedAudioSystem);

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, audioSystemNames[selectedAudioSystem]);


        GUILayout.Label("Conductors:", EditorStyles.boldLabel);

        Conductor[] conductorsInScene = FindObjectsOfType<Conductor>();

        foreach (Conductor conductor in conductorsInScene)
        {
            if (EditorApplication.isPlaying)
            {
                GUILayout.BeginVertical("box");
                AudioSystem audioSystem = conductor.song;
                string bpm = conductor.bpm + " BPM";
                GUILayout.Label(audioSystem.GetAudioName() + " - " + bpm);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Play"))
                {
                    audioSystem.Play();
                }
                if (GUILayout.Button("Pause"))
                {
                    audioSystem.Pause();
                }
                if (GUILayout.Button("Stop"))
                {
                    audioSystem.Stop();
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("Enter play mode to edit conductor(s).", MessageType.Info);
            }
        }

        if (GUILayout.Button("Create Conductor"))
        {
            GameObject conductorPrefab = LoadConductorPrefab();
            GameObject instance = PrefabUtility.InstantiatePrefab(conductorPrefab) as GameObject;
            instance.transform.position = Vector3.zero;
        }

        if (conductorsInScene.Length == 0)
        {
            EditorGUILayout.HelpBox("No conductors found in the scene!", MessageType.Error);
        }
        else if (conductorsInScene.Length > 1)
        {
            EditorGUILayout.HelpBox(conductorsInScene + " Conductors found in the scene. Rhythm tools supports multiple, just make sure you keep track of them!", MessageType.Warning);
        }
    }

    private GameObject LoadConductorPrefab()
    {
        return AssetDatabase.LoadAssetAtPath<GameObject>(CONDUCTOR_PREFAB_PATH);
    }
}
