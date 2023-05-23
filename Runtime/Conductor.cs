/*
 * Conductor.cs
 * Referenced from this great article: https://www.reddit.com/r/gamedev/comments/2fxvk4/heres_a_quick_and_dirty_guide_i_just_wrote_how_to/
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    /// <summary>
    /// The song's beats per minute
    /// </summary>
    public float bpm = 120f;
    /// <summary>
    /// The amount of time (in seconds) that has passed since the start of the beat.
    /// </summary>
    public float crotchet
    {
        get; private set;
    }
    /// <summary>
    /// The percentage of time that has passed within the current beat.
    /// The start of the beat is 0 while the end of the beat is 1.
    /// </summary>
    public float crotchetNormalized
    {
        get; private set;
    }
    /// <summary>
    /// The position of the song since the beginning of the first beat.
    /// </summary>
    public float songPosition
    {
        get; private set;
    }
    /// <summary>
    /// The amount of time since the last conductor update.
    /// </summary>
    public float deltaSongPosition
    {
        get; private set;
    }
    /// <summary>
    /// The amount of time between the start of the audio file and the first beat of the song.
    /// </summary>
    public float offset = 0f;
    /// <summary>
    /// The amount of time any given beat lasts for.
    /// </summary>
    public float beatDuration
    {
        get; private set;
    }
    /// <summary>
    /// The current beat number the song is on.
    /// </summary>
    public int beat
    {
        get; private set;
    }
    /// <summary>
    /// The amount of time the song will delay before playing.
    /// </summary>
    public float countdownToStart = 3f;

    /// <summary>
    /// The pitch (speed) that the song will play at.
    /// This is obtained directly from the AudioSource connected to this object.
    /// </summary>
    public float pitch
    {
        get; private set;
    }

    private AudioSource song;
    private AudioSource beatTick;

    private bool countdownFinished;

    void Awake()
    {
        countdownFinished = false;
        song = GetComponent<AudioSource>();
        beat = 0;
        crotchet = 0;
        crotchetNormalized = 0f;
        beatDuration = 60f / bpm;
        beatTick = transform.GetChild(0).GetComponent<AudioSource>();
        pitch = song.pitch;
        if (!song.playOnAwake)
        {
            StartCountdown();
        }
        else
        {
            countdownFinished = true;
        }
    }
    
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
        crotchetNormalized = crotchet / beatDuration;

        if (crotchet >= beatDuration)
        {
            beat++;
            beatTick.Play();
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