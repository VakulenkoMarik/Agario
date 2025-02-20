// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.Audio;

namespace Agario.Scripts.Engine;

public class Game
{
    private readonly GameLoop gameLoop;

    public Game()
    {
        ProgramConfig.Init();
        
        gameLoop = new();
        ServiceLocatorInit();
    }
    
    private void ServiceLocatorInit()
    {
        ServiceLocator.Instance.Register(new AudioSystem());
        ServiceLocator.Instance.Register(new PauseActivator());
    }

    public void Start()
        => gameLoop.Run();
}
