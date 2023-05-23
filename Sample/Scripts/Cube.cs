using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Animator animator;
    private Conductor conductor;
    // Start is called before the first frame update
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        animator = GetComponent<Animator>();
        animator.speed = conductor.bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = conductor.bpm / 60 * conductor.pitch;
    }
}
