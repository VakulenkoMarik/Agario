// ReSharper disable InconsistentNaming
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Scene;

public static class SceneLoader
{
    private static readonly Queue<Scene> scenesToOpen = new();
    private static readonly Dictionary<string, Scene> scenes = new();
    public static Scene CurrentScene { get; private set; }

    public static RenderWindow Window { get; private set; }

    public static void Init(RenderWindow window)
        => Window = window;

    public static void AddOnExitSceneAction(Action action)
        => CurrentScene.AddOnExitSceneAction(action);

    public static void AddOnExitGameAction(Action action)
        => Window.Closed += (_, _) => action();

    public static void Start(string firstSceneName)
    {
        scenesToOpen.Enqueue(GetScene(firstSceneName));
        
        while (scenesToOpen.Count > 0 && Window.IsOpen)
        {
            Scene newCurrentScene = scenesToOpen.Dequeue();
            
            CurrentScene = newCurrentScene;
            
            newCurrentScene.Start();
        }
    }
    
    public static Scene AddScene(string sceneName, ISceneRules rules)
    {
        Scene newScene = new Scene(rules, Window);
        scenes.Add(sceneName, newScene);
        
        return newScene;
    }

    public static Scene GetScene(string name)
        => scenes[name];

    public static void LoadScene(string sceneName)
    {
        CurrentScene.Stop();
        
        Scene target = GetScene(sceneName);
        
        scenesToOpen.Enqueue(target);
    }
}