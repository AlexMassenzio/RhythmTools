/*
 * Conductor.cs
 * Referenced from this great article: https://www.reddit.com/r/gamedev/comments/2fxvk4/heres_a_quick_and_dirty_guide_i_just_wrote_how_to/
*/

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// A class that synchronizes game events with a music track.
/// </summary>
public class Conductor : MonoBehaviour
{
    /// <summary>
    /// The beats per minute of the music track, accounting for pitch.
    /// </summary>
    public float bpm = 120f;
    /// <summary>
    /// The beats per minute of the music track, not accounting for pitch.
    /// </summary>
    public float baseBpm
    {
        get; private set;
    }
    public int beat
    {
        get; private set;
    }
    /// <summary>
    /// The amount of time any given beat lasts for.
    /// </summary>
    public float beatDuration
    {
        get; private set;
    }
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
        get { return crotchet / beatDuration; }
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
    /// The current beat number the song is on.
    /// </summary>
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
        beatDuration = 60f / bpm;
        crotchet = 0;
        beatTick = transform.GetChild(0).GetComponent<AudioSource>();
        baseBpm = bpm;
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
        if (song.isPlaying)
        {
            pitch = song.pitch;
            bpm = baseBpm * pitch;

            float oldSongPosition = songPosition;
            if (countdownFinished)
            {
                songPosition = song.time - offset;
            }
            deltaSongPosition = songPosition - oldSongPosition;

            crotchet = songPosition - (beatDuration * beat);

            if (crotchetNormalized >= 1 || crotchetNormalized <= 0)
            {
                beat = (int)(songPosition / beatDuration);
                beatTick.Play();
            }
        }
        //If we reversed to the end of the song...
        else if (song.pitch < 0f)
        {
            Debug.Log("setting time.");
            song.time = song.clip.length - 0.01f;
            song.Play();
        }
    }

    /// <summary>
    /// Returns a string with debug information about the current state of the conductor.
    /// </summary>
    /// <returns>A string with debug information.</returns>
    public string GetDebugText()
    {
        var builder = new StringBuilder();
        builder
            .Append("Song Position: " + songPosition).AppendLine()
            .Append("Current BPM: " + bpm).AppendLine()
            .Append("Base BPM: " + baseBpm).AppendLine()
            .Append("Crotchet: " + crotchet).AppendLine()
            .Append("Crotchet Normalized: " + crotchetNormalized).AppendLine()
            .Append("Beat: " + beat).AppendLine();
        return builder.ToString();
    }

    /// <summary>
    /// Adds a countdown to the beginning of the music track.
    /// </summary>
    private void StartCountdown()
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