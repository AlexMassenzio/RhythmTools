/*
 * Conductor.cs
 * Referenced from this great article: https://www.reddit.com/r/gamedev/comments/2fxvk4/heres_a_quick_and_dirty_guide_i_just_wrote_how_to/
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{

    public float bpm = 120f;
    public float crotchet;
    public float songPosition;
    public float deltaSongPosition;
    public float offset = 0f; //positive means the song must be minussed.
    public float beatDuration;
    public int beat;
    public float countdownToStart = 3f;

    public float pitch;

    private AudioSource song;
    private AudioSource beatTick;

    private bool countdownFinished;

    // Use this for initialization
    void Start()
    {
        countdownFinished = false;
        song = GetComponent<AudioSource>();
        beat = 0;
        crotchet = 0;
        beatDuration = 60f / bpm;
        beatTick = transform.GetChild(0).GetComponent<AudioSource>();
        pitch = song.pitch;
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        pitch = song.pitch;
        float oldSongPosition = songPosition;
        if (countdownFinished)
        {
            songPosition = song.time - offset;
        }
        deltaSongPosition = songPosition - oldSongPosition;

        crotchet = songPosition - (beatDuration * beat);

        if (crotchet >= beatDuration)
        {
            beat++;

            //beatTick.Play();
        }

        if(song.clip.length - songPosition < 0.5)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void StartCountdown()
    {
        StartCoroutine(StartCountdownHelper());
    }

    IEnumerator StartCountdownHelper()
    {
        float timer = countdownToStart * -1f;
        while (timer < 0f - offset)
        {
            timer += Time.deltaTime * pitch;
            songPosition = timer;
            yield return null;
        }
        countdownFinished = true;
        song.Play();
    }
}