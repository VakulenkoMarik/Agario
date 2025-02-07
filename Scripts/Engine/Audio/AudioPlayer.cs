using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using SFML.Audio;
using Sini;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine.Audio;

public class AudioPlayer
{
    public static AudioPlayer Instance { get; private set; } = null!;
    private readonly Dictionary<string, Sound> sounds = new();

    public AudioPlayer()
    {
        GameLoop gl = GameLoop.GetInstance();
        gl.AddEndLoopAction(Dispose);
    }

    public void Dispose()
    {
        foreach (Sound s in sounds.Values)
        {
            s.TurnOff();
        }
    }

    public static void SetInstance(AudioPlayer audioPlayer)
    {
        Instance = audioPlayer;
    }

    public Sound? TryGet(string name)
    {
        return sounds.GetValueOrDefault(name);
    }

    public void Play(string name)
    {
        if (sounds.TryGetValue(name, out var toPlay))
        {
            toPlay.Play();
        }
    }

    public void Init()
    {
        var ini = new IniFile(PathUtils.GetGameIniFile());
        var keys = ini.GetKeys("Audio");
        
        foreach (var key in keys)
        {
            string path = ini.GetStr("Audio", key, "");

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    var sound = new Sound();
                    sound.Init(path);
                    
                    sounds.Add(key, sound);
                }
                catch (Exception)
                {
                    throw new ApplicationException();
                }
            }
        }
    }
}