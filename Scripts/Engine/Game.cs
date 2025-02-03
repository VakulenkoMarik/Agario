using Agario.Scripts.Engine.Interfaces;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class Game : IUpdatable
{
    private readonly GameLoop gameLoop;
    private IGameRules gameRules = null!;

    public Game()
    {
        GameInit();
        
        gameLoop = new();
        
        gameLoop.updatableObjects.Add(this);
    }

    private void GameInit()
    {
        ProgramConfig.Init();
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
