// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Audio;
using Agario.Scripts.Engine.Interfaces;
using SFML.Audio;

namespace Agario.Scripts.Game.Audio;

public enum AudioType
{
    SomeoneWasKilled,
    Movement,
    Happy,
    Running,
    Background,
}

public class AudioSystem : IGameUtility
{
    private float listenerPresetVolume = Listener.GlobalVolume;

    private AudioMethods methods;
    
    private readonly AudioPlayer audioPlayer;

    public AudioSystem()
    {
        audioPlayer = new();
        audioPlayer.Load();

        methods = new AudioMethods(audioPlayer);
    }

    public void Play(AudioType type)
    {
        Action toPlay = type switch
        {
            AudioType.SomeoneWasKilled => methods.SomeoneWasKilled,
            AudioType.Movement => methods.Movement,
            AudioType.Background => methods.Background,
            AudioType.Running => methods.Running,
            AudioType.Happy => methods.Happy,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        toPlay.Invoke();
    }

    public void SetVolume(string name, int value)
    {
        Sound? sound = audioPlayer.TryGet(name);

        if (sound is not null)
        {
            sound.Volume = value;
        }
    }

    public void Mute(bool isMute)
    {
        if (isMute)
        {
            listenerPresetVolume = Listener.GlobalVolume;
            Listener.GlobalVolume = 0;

            return;
        }

        Listener.GlobalVolume = listenerPresetVolume;
    }
}