using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Game;
using Agario.Scripts.Game.ScenesRules;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Engine.Game game = new Engine.Game();
        SceneLoader.Init("MainMenu", new MainMenu());
        SceneLoader.AddScene("Game", new AgarioGame());
        
        game.Start();
    }
}