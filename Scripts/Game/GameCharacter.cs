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
    
    public List<string> AnimationsNames { get; set; } = new();
    public List<string> SoundsNames { get; set; } = new();
}

public class GameCharacter(GameCharacterData data)
{
    public readonly string Name = data.Name;
    public Texture IconTexture = new(PathUtils.Get(data.IconTexturePath));
    
    public Dictionary<string, Animation> Animations = new();
    public Dictionary<string, Sound> Sounds = new();

    public readonly GameCharacterData Data = data;

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