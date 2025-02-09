// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Audio;
using SFML.Audio;
using SFML.System;

namespace Agario.Scripts.Game.Audio;

public enum AudioType
{
    SomeoneWasKilled,
    Movement,
}

public class AudioSystem
{
    public static AudioSystem Instance { get; private set; } = null!;

    private float listenerPresetVolume = Listener.GlobalVolume;

    private AudioMethods methods;

    public AudioSystem()
    {
        AudioPlayer audioPlayer = new();
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
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        toPlay.Invoke();
    }

    public void Play(AudioType type, params object[] args)
    {
        Action<object[]> toPlay = type switch
        {
            AudioType.Movement => parameters => methods.Movement((Vector2f)parameters[0]),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        toPlay.Invoke(args);
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