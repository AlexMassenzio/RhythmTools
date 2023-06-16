#if RT_USE_FMOD
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class FMODAudioSystem : AudioSystem
{
    private StudioEventEmitter eventEmitter;
    private EventInstance eventInstance;

    public FMODAudioSystem(StudioEventEmitter eventEmitter)
    {
        this.eventEmitter = eventEmitter;
        this.eventInstance = eventEmitter.EventInstance;
    }

    /// <summary>
    /// Returns the speed of the audio. 1 is normal speed, 2 is double speed, 0.5 is half speed.
    /// GetSpeed() is not implemented for FMOD. Returning 1 as default.
    /// </summary>
    public float GetSpeed()
    {
        //GetSpeed() is not implemented for FMOD. Returning 1 as default.
        eventEmitter.EventInstance.getPitch(out float pitch);
        return 1;
    }

    public void Play()
    {
        if (!eventInstance.isValid())
        {
            eventEmitter.Play();
            eventInstance = eventEmitter.EventInstance;
        }
        else
        {
            eventInstance.setPaused(false);
        }
    }

    public void Stop()
    {
        eventEmitter.Stop();
    }

    public void Pause()
    {
        if (eventInstance.isValid())
        {
            eventEmitter.EventInstance.setPaused(true);
        }
    }

    public bool IsPlaying()
    {
        if (eventInstance.isValid())
        {
            PLAYBACK_STATE state;
            eventInstance.getPlaybackState(out state);
            return state == PLAYBACK_STATE.PLAYING;
        }
        else
        {
            return false;
        }
    }

    public string GetAudioName()
    {
        return eventEmitter.name;
    }

    public float GetAudioLength()
    {
        eventEmitter.EventDescription.getLength(out int length);
        return length;
    }

    public float GetTime()
    {
        eventEmitter.EventInstance.getTimelinePosition(out int time);
        float timeInSeconds = time / 1000f;
        return timeInSeconds;
    }

    public void SetTime(float time)
    {
        int timeInMilliseconds = (int)(time * 1000);
        eventEmitter.EventInstance.setTimelinePosition(timeInMilliseconds);
    }
}
#endif