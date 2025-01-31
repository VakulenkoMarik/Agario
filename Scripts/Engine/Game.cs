using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class Game : IUpdatable
{
    private readonly GameLoop gameLoop;
    private IGameRules gameRules = null!;

    public Game()
    {
        Configurations.Init();
        
        gameLoop = new();
        
        gameLoop.updatableObjects.Add(this);
    }

    public void SetGameRules(IGameRules rules)
    {
        gameRules = rules;
    }

    public void Start()
    {
        gameLoop.Run();
    }

    public void Update()
    {
        gameRules.Update();
    }
}
