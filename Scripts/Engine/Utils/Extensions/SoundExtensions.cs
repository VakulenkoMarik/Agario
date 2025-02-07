using SFML.Audio;

namespace Agario.Scripts.Engine.Utils.Extensions;

public static class SoundExtensions
{
    public static void Init(this Sound sound, string path)
    {
        SoundBuffer bounceSoundBuffer = new SoundBuffer(PathUtils.Get(path));
        sound.SoundBuffer = bounceSoundBuffer;
    }

    public static void TurnOff(this Sound sound)
    {
        sound.Dispose();
        sound.SoundBuffer.Dispose();
    }
}