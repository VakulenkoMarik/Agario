using Agario.Scripts.Engine.Scene;
using Agario.Scripts.SeaBattleGame.Rules;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Engine.Game game = new Engine.Game();
        
        SceneLoader.Init(game);
        SceneLoader.AddScene("Game", new SeaBattle());
        
        SceneLoader.LoadScene("Game");
    }
}