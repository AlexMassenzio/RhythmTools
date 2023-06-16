/// <summary>
/// An interface for an audio source that can be used by the conductor.
/// </summary>
public interface AudioSystem
{
    /// <summary>
    /// The pitch (speed) that the song will play at.
    /// </summary>
    float GetSpeed();
    /// <summary>
    /// Starts playing the song.
    /// </summary>
    void Play();
    /// <summary>
    /// Stops playing the song.
    /// </summary>
    void Stop();
    /// <summary>
    /// Pauses the song.
    /// </summary>
    void Pause();
    /// <summary>
    /// Returns true if the song is currently playing.
    /// </summary>
    bool IsPlaying();
    /// <summary>
    /// The name of the audio clip.
    /// </summary>
    string GetAudioName();
    /// <summary>
    /// The length of the audio clip in seconds.
    /// </summary>
    float GetAudioLength();
    /// <summary>
    /// The current position of the song in seconds.
    /// </summary>
    float GetTime();
    /// <summary>
    /// Sets the current position of the song in seconds.
    /// </summary>
    void SetTime(float time);
}