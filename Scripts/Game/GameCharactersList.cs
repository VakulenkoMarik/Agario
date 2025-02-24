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

    private void LoadCharacters()
    {
        var loadedCharacters = JsonHandler.LoadData<List<GameCharacterData>>("GameCharacters.json");
        
        if (loadedCharacters is not null)
        {
            foreach (var data in loadedCharacters)
            {
                GameCharacter character = new(data);
                
                var animations = ServiceLocator.Instance.Get<AnimationsList>().Animations;
                character.FillCharacterAnimations(data.AnimationsNames, animations);
                
                var soundPaths = JsonHandler.LoadData<Dictionary<string, string>>("Sounds.json");
                
                if (soundPaths is not null)
                    character.FillCharacterSounds(data.SoundsNames, soundPaths);
                
                Characters.Add(character);
            }
        }
    }
}