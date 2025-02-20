using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Game;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Engine.Game game = new Engine.Game();
        SceneLoader.Init("Game");

        AgarioGame mainGame = new AgarioGame();
        game.Start();
    }
}