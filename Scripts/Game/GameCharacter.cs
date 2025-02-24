using Agario.Scripts.Engine.Animations;
using SFML.Audio;
using SFML.Graphics;

namespace Agario.Scripts.Game;

[Serializable]
public struct GameCharacterData
{
    public string Name;
    public string IconTexturePath;
    
    public List<string> AnimationsNames;
    public List<string> SoundsNames;
}

public class GameCharacter(GameCharacterData data)
{
    public readonly string Name = data.Name;
    public Texture IconTexture = new(data.IconTexturePath);
    
    public readonly Dictionary<string, Animation> Animations = new();
    public readonly Dictionary<string, Sound> Sounds = new();

    public void FillCharacterAnimations(List<string> animatonsNames, Dictionary<string, Animation> allAnimations)
    {
        foreach (var key in animatonsNames)
        {
            if (allAnimations.TryGetValue(key, out Animation? animation))
            {
                Animations[key] = animation;
            }
        }
    }

    public void FillCharacterSounds(List<string> soundsNames, Dictionary<string, string> allSoundsPaths)
    {
        foreach (var name in soundsNames)
        {
            if (allSoundsPaths.TryGetValue(name, out string? path))
                Sounds[name] = new Sound(new SoundBuffer(path));
        }
    }
}