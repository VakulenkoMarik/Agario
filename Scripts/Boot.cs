using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.GameProcess;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        
        IGameRules rules = new AgarioGame();
        game.SetGame(rules);
        
        game.Start();
    }
}