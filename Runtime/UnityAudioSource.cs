using UnityEngine;

public class UnityAudioSource : RTAudioSource
{
    AudioSource audioSource;

    public UnityAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    public float GetSpeed()
    {
        return audioSource.pitch;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public float GetLength()
    {
        return audioSource.clip.length;
    }

    public float GetTime()
    {
        return audioSource.time;
    }

    public void SetTime(float time)
    {
        audioSource.time = time;
    }
}