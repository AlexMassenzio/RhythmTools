using System;
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
        float animatorSpeed = Math.Abs(conductor.bpm / 60);

        // Unity does not let you set negative animator speed, so we have to use a multiplier instead...
        if (conductor.bpm < 0)
        {
            animator.SetFloat("multiplier", -1f);
        }
        else
        {
            animator.SetFloat("multiplier", 1f);
        }

        if (!conductor.song.IsPlaying())
        {
            animatorSpeed = 0f;
        }

        animator.speed = animatorSpeed;
    }
}
