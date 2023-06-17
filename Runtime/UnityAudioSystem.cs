using UnityEngine;

public class UnityAudioSystem : IAudioSystem
{
    AudioSource audioSource;

    public UnityAudioSystem(AudioSource audioSource)
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
    public string GetAudioName()
    {
        return audioSource.clip.name;
    }
    public float GetAudioLength()
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