// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Engine.Utils;

namespace Agario.Scripts.Engine;

public class Game
{
    private readonly GameLoop gameLoop;

    public Game()
    {
        FilesInit();
        
        gameLoop = new();
        
        ObjectsInit();
    }

    private void FilesInit()
    {
        ProgramConfig.Init();
    }

    private void ObjectsInit()
    {
        SceneLoader.Init(gameLoop);
        
        ServiceLocatorInit();
    }
    
    private void ServiceLocatorInit()
    {
        ServiceLocator.Instance.Register(new PauseActivator());
    }

    public void Start()
    {
        try
        {
            string? currentSceneName = SceneLoader.CurrentScene?.Name;
            SceneLoader.LoadScene(currentSceneName);
        
            gameLoop.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine("You cannot start the game: " + e);
            throw;
        }
    }
}
