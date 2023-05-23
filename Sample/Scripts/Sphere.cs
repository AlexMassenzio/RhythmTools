using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Conductor conductor;

    // Start is called before the first frame update
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
    }

    // Update is called once per frame
    void Update()
    {
        float scale = conductor.crotchetNormalized + 1;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
