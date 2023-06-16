using UnityEngine;
using UnityEngine.UI;

public class ConductorDebug : MonoBehaviour
{

    public Conductor conductor;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = conductor.GetDebugText();
    }
}
