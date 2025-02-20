using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;

namespace Agario.Scripts.Engine.Scene;

public class Scene(string name)
{
    public readonly string Name = name;
    
    private readonly List<IUpdatable> updatableObjects = new();
    private readonly List<IDrawable> drawableObjects = new();
    public Dictionary<string, EventKey> InputKeys = new();
    
    public void SetThisAsCurrentScene()
        => SceneLoader.SetCurrentScene(Name);
    
    public void AddUpdatableObject(IUpdatable updatable)
        => updatableObjects.Add(updatable);
    
    public void AddDrawableObject(IDrawable drawable)
        => drawableObjects.Add(drawable);
    
    public void DestroyUpdatableObject(IUpdatable updatable)
        => updatableObjects.RemoveSwap(updatable);
    
    public void DestroyDrawableObject(IDrawable drawable)
        => drawableObjects.RemoveSwap(drawable);
    
    public void LoadDataToGameLoop()
    {
        GameLoop gl = GameLoop.GetInstance();
        gl.SetData(updatableObjects, drawableObjects);

        Input.SetKeys(InputKeys);
    }
}