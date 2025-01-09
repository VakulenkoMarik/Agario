using Agario.Scripts.GameProcess;

namespace Agario.Scripts;

public class Boot
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}