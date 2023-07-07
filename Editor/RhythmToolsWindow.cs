using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace RhythmTools.Editor
{
    public enum AUDIO_SYSTEM
    {
        Unity,
        FMOD
    }

    public class RhythmToolsWindow : EditorWindow
    {
        private const string CONDUCTOR_PREFAB_PATH = "Packages/com.alexmassenzio.rhythmtools/Runtime/Prefabs/";
        private AUDIO_SYSTEM selectedAudioSystem;

        private Dictionary<AUDIO_SYSTEM, string> audioSystemDefineSymbols = new Dictionary<AUDIO_SYSTEM, string>()
    {
        { AUDIO_SYSTEM.Unity, "RT_USE_UNITY" },
        { AUDIO_SYSTEM.FMOD, "RT_USE_FMOD" }
    };

        private Dictionary<AUDIO_SYSTEM, string> conductorPrefabs = new Dictionary<AUDIO_SYSTEM, string>()
    {
        { AUDIO_SYSTEM.Unity, "Conductor.prefab" },
        { AUDIO_SYSTEM.FMOD, "FMODConductor.prefab" }
    };

        [MenuItem("Window/RhythmTools")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<RhythmToolsWindow>("RhythmTools");
        }

        private void OnGUI()
        {
            selectedAudioSystem = (AUDIO_SYSTEM)EditorGUILayout.EnumPopup("Audio System", LoadAudioSystemSelection());

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, audioSystemDefineSymbols[selectedAudioSystem]);

            GUILayout.Label("Conductors:", EditorStyles.boldLabel);

            Conductor[] conductorsInScene = FindObjectsOfType<Conductor>();

            foreach (Conductor conductor in conductorsInScene)
            {
                if (EditorApplication.isPlaying)
                {
                    GUILayout.BeginVertical("box");
                    IAudioSystem audioSystem = conductor.song;
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
                    if (selectedAudioSystem == AUDIO_SYSTEM.FMOD)
                    {
                        EditorGUILayout.HelpBox("FMOD Conductor is in an experimental state and most likely will not work!", MessageType.Warning);
                    }
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
            string fullConductorPrefabPath = CONDUCTOR_PREFAB_PATH + conductorPrefabs[selectedAudioSystem];
            return AssetDatabase.LoadAssetAtPath<GameObject>(fullConductorPrefabPath);
        }

        private AUDIO_SYSTEM LoadAudioSystemSelection()
        {
            string defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            foreach (KeyValuePair<AUDIO_SYSTEM, string> pair in audioSystemDefineSymbols)
            {
                if (EqualityComparer<string>.Default.Equals(pair.Value, defineSymbols))
                {
                    return pair.Key;
                }
            }
            return AUDIO_SYSTEM.Unity;
        }
    }
}
