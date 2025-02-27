// ReSharper disable InconsistentNaming
#pragma warning disable CS8618, CS9264

using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.Engine;

public class Game
{
    public GameLoop? gameLoop { get; private set; }
    public RenderWindow GameWindow { get; private set; }

    public Game()
    {
        FilesInit();
        
        WindowInit();
        
        ObjectsInit();
    }

    private void WindowInit()
    {
        uint width = (uint)ProgramConfig.Data.WindowWidth;
        uint height = (uint)ProgramConfig.Data.WindowHeight;
        
        GameWindow = new RenderWindow(new VideoMode(width, height), "Game window");
        
        GameWindow.Closed += (_, _) => GameWindow.Close();
    }

    private void FilesInit()
    {
        ProgramConfig.Init();
    }

    private void ObjectsInit()
    {
        SceneLoader.Init(this);
        ServiceLocator.Instance.Register(new PauseActivator());
    }

    public void NewGameLoop()
    {
        gameLoop?.Stop();
        gameLoop = new(GameWindow);
    }
}
