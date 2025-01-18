using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Game;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Engine.Game game = new();
        IGameRules rules = new AgarioGame();
        
        game.SetGameRules(rules);
        
        game.Start();
    }
}