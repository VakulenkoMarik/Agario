using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Game.ScenesRules;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Engine.Game game = new Engine.Game();
        
        SceneLoader.AddScene("MainMenu", new MainMenu());
        SceneLoader.AddScene("Game", new AgarioGame());
        
        SceneLoader.Start("MainMenu");
    }
}