using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;

namespace Agario.Scripts.Game;

public class GameCharactersList : IGameUtility
{
    public List<GameCharacter> Characters { get; private set; } = new();

    public GameCharactersList()
    {
        LoadCharacters();
    }

    public GameCharacter? TryGetNewCharacter(string name)
    {
        foreach (var character in Characters)
        {
            if (character.Name.Equals(name))
            {
                return new GameCharacter(character.Data)
                {
                    Animations = character.Animations,
                    Sounds = character.Sounds,
                };
            }
        }

        return null;
    }

    private void LoadCharacters()
    {
        var loadedCharacters = JsonHandler.LoadData<List<GameCharacterData>>(@"Resources/Files/JSONs/GameCharacters.json");
        
        if (loadedCharacters is not null)
        {
            foreach (var data in loadedCharacters)
            {
                GameCharacter character = new(data);
                
                var animations = ServiceLocator.Instance.Get<AnimationsList>().Animations;
                character.FillCharacterAnimations(data.AnimationsNames, animations); 
                
                var soundPaths = JsonHandler.LoadData<Dictionary<string, string>>(@"Resources/Files/JSONs/Sounds.json");
                
                if (soundPaths is not null)
                    character.FillCharacterSounds(data.SoundsNames, soundPaths);
                
                Characters.Add(character);
            }
            
            
        }
    }
}