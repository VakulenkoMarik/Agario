using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Utils;
using SFML.Audio;
using SFML.Graphics;

namespace Agario.Scripts.Game;

[Serializable]
public class GameCharacterData
{
    public string Name { get; set; }
    public string IconTexturePath { get; set; }
    
    public List<string> AnimationsNames = new();
    public List<string> SoundsNames = new();
}

public class GameCharacter
{
    public GameCharacter(GameCharacterData data)
    {
        Name = data.Name;
        IconTexture = new(PathUtils.Get(data.IconTexturePath));
    }
    
    public readonly string Name;
    public readonly Texture IconTexture;
    
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