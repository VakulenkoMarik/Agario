// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.InputSystem;

namespace Agario.Scripts.Engine.Scene;

public static class SceneLoader
{
    private static Scene currentScene = null!;
    private static readonly List<Scene> scenes = new();

    public static void Init(string sceneName)
    {
        Scene scene = AddScene(sceneName);
        currentScene = scene;
        
        scene.LoadDataToGameLoop();
    }
    
    public static Scene GetCurrentScene()
        => currentScene;
    
    public static void SetCurrentScene(string sceneName)
    {
        currentScene.InputKeys = Input.Keys;
        
        Scene? scene = GetScene(sceneName);

        if (scene is not null)
        {
            currentScene = scene;
            currentScene.LoadDataToGameLoop();
            
            return;
        }

        throw new ArgumentException("Scene not found");
    }
    
    private static void StartScene(Scene scene)
        => scene.SetThisAsCurrentScene();
    
    public static Scene AddScene(string sceneName)
    {
        Scene newScene = new Scene(sceneName);
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