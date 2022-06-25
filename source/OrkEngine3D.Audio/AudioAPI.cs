using CSCore.Codecs.WAV;
using CSCore.SoundOut;
using OrkEngine3D.Graphics.TK.Resources;

namespace OrkEngine3D.Audio;
using CSCore;
public static class AudioApi
{
    private static readonly Dictionary<ID, WaveOut> Sources = new Dictionary<ID, WaveOut>();

    public static ID CreateTrack(string file)
    {
        ID id = new ID();
        IWaveSource source = new WaveFileReader(file);
        
        WaveOut output = new WaveOut();
        output.Initialize(source);
        
        Sources.Add(id, output);
        return id;
    }

    public static void Play(ID source) => Sources[source].Play();
    public static void Pause(ID source) => Sources[source].Pause();
    public static void Resume(ID source) => Sources[source].Resume();
    public static void Stop(ID source) => Sources[source].Stop();

    public static bool Playing(ID source)
    {
        return Sources[source].PlaybackState == PlaybackState.Playing;
    }
    
    public static float GetVolume(ID source)
    {
        return Sources[source].Volume;
    }
    
    public static void SetVolume(ID source, float volume)
    {
        Sources[source].Volume = volume;
    }
    
    
}