using Agario.Scripts.Engine;

namespace Agario.Scripts.GameProcess;

public class Game
{
    private GameLoop gameLoop;

    public Game()
    {
        gameLoop = new GameLoop(this);
    }
    
    public void Start()
    {
        gameLoop.Run();
    }
    
    public void Update()
    {
        
    }
}