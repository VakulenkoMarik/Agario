// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Scene;

public static class SceneLoader
{
    public static Scene? CurrentScene { get; private set; }

    private static GameLoop gameLoop => targetGame.gameLoop;
    private static readonly Dictionary<string, Scene> scenes = new();

    public static RenderWindow Window { get; private set; }
    private static Game targetGame;

    public static void Init(Game game)
    {
        targetGame = game;
        Window = targetGame.GameWindow;
    }

    public static void AddOnExitSceneAction(Action action)
        => gameLoop.AddEndSceneAction(action);

    public static void AddOnExitGameAction(Action action)
        => Window.Closed += (_, _) => action();

    private static void StartScene(Scene scene)
    {
        targetGame.NewGameLoop();
        
        LastSceneDeactivate();

        CurrentScene = scene;
        CurrentSceneDataToGameLoop();
        
        scene.Start();
        gameLoop.Run();
    }

    private static void LastSceneDeactivate()
    {
        Scene? last = CurrentScene;
        Input.Keys.Clear();
        last?.Deactivate();
    }

    private static void CurrentSceneDataToGameLoop()
    {
        if (CurrentScene is not null)
        {
            (List<IUpdatable> updatables, List<IDrawable> drawables) = CurrentScene.GetData();
        
            gameLoop.SetData(updatables, drawables);
        }
    }
    
    public static Scene AddScene(string sceneName, ISceneRules rules)
    {
        Scene newScene = new Scene(rules);
        scenes.Add(sceneName, newScene);
        
        return newScene;
    }

    public static Scene? GetScene(string name)
        => scenes.GetValueOrDefault(name);

    public static void LoadScene(string sceneName)
    {
        Scene? target = GetScene(sceneName);
        
        if (target is not null)
        {
            StartScene(target);
            return;
        }

        throw new ArgumentException("Scene not found");
    }
}