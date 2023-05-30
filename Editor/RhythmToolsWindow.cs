using UnityEditor;
using UnityEngine;

public class RhythmToolsWindow : EditorWindow
{
    private const string CONDUCTOR_PREFAB_PATH = "Packages/com.alexmassenzio.rhythmtools/Runtime/Conductor.prefab";

    [MenuItem("Window/RhythmTools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<RhythmToolsWindow>("RhythmTools");
    }

    private void OnGUI()
    {
        GUILayout.Label("Conductors:", EditorStyles.boldLabel);

        Conductor[] conductorsInScene = FindObjectsOfType<Conductor>();
        foreach (Conductor conductor in conductorsInScene)
        {
            AudioSource audioSource = conductor.GetComponent<AudioSource>();
            string bpm = conductor.bpm + " BPM";
            GUILayout.Label(audioSource.clip.name + " - " + bpm);
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
