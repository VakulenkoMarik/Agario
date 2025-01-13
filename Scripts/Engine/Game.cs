using Agario.Scripts.Engine.Interfaces;

namespace Agario.Scripts.Engine;

public abstract class Game : IUpdatable
{
    private GameLoop gameLoop;

    public Game()
    {
        gameLoop = new GameLoop();
        
        gameLoop.updatableObjects.Add(this);
    }

    public void Start()
    {
        gameLoop.Run();
    }

    public virtual void Update()
    {
        // Place the logic that should be updated every game turn here
    }
}
