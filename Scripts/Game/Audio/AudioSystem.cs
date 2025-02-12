// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Audio;
using SFML.Audio;

namespace Agario.Scripts.Game.Audio;

public enum AudioType
{
    SomeoneWasKilled,
    Movement,
    Background,
}

public class AudioSystem
{
    public static AudioSystem Instance { get; private set; } = null!;

    private float listenerPresetVolume = Listener.GlobalVolume;

    private AudioMethods methods;
    
    private readonly AudioPlayer audioPlayer;

    public AudioSystem()
    {
        audioPlayer = new();
        audioPlayer.Load();

        methods = new AudioMethods(audioPlayer);
    }

    public static void SetInstance(AudioSystem system)
    {
        Instance = system;
    }

    public void Play(AudioType type)
    {
        Action toPlay = type switch
        {
            AudioType.SomeoneWasKilled => methods.SomeoneWasKilled,
            AudioType.Movement => methods.Movement,
            AudioType.Background => methods.Background,
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