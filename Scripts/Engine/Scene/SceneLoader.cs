// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Interfaces;

namespace Agario.Scripts.Engine.Scene;

public static class SceneLoader
{
    public static Scene? CurrentScene { get; private set; }

    private static GameLoop gameLoop = null!;
    private static readonly List<Scene> scenes = new();

    public static void Init(GameLoop loop)
    {
        gameLoop = loop;
        loop.AddEndLoopAction(OnExitGame);
    }

    public static void AddOnExitGameAction(Action action)
        => gameLoop.AddEndLoopAction(action);

    private static void OnExitGame()
        => CurrentScene?.Deactivate();

    public static void InitFirstScene(string sceneName, ISceneRules rules)
    {
        Scene scene = AddScene(sceneName, rules);
        CurrentScene = scene;
    }

    private static void StartScene(Scene scene)
    {
        Scene? last = CurrentScene;

        CurrentScene = scene;
        scene.LoadDataToGameLoop();
        
        last?.Deactivate();

        if (!scene.IsInited)
        {
            scene.SceneRules.Init();
            scene.IsInited = true;
        }
        
        scene.SceneRules.Start();
    }
    
    public static Scene AddScene(string sceneName, ISceneRules rules)
    {
        Scene newScene = new Scene(sceneName, rules, gameLoop);
        scenes.Add(newScene);
        
        return newScene;
    }

    public static Scene? GetScene(string name)
    {
        foreach (var scene in scenes)
        {
            if (scene.Name.Equals(name))
            {
                return scene;
            }
        }

        return null;
    }

    public static void LoadScene(string sceneName)
    {
        foreach (var scene in scenes)
        {
            if (scene.Name.Equals(sceneName))
            {
                StartScene(scene);
                return;
            }
        }

        throw new ArgumentException("Scene not found");
    }
}