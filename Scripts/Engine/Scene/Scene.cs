using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;

namespace Agario.Scripts.Engine.Scene;

public class Scene(string name, ISceneRules rules, GameLoop loop)
{
    public readonly string Name = name;
    public bool IsInited = false;
    
    private readonly List<IUpdatable> updatableObjects = new();
    private readonly List<IDrawable> drawableObjects = new();

    public ISceneRules SceneRules { get; private set; } = rules;

    public void Update()
        => SceneRules.Update();
    
    public void AddUpdatableObject(IUpdatable updatable)
        => updatableObjects.Add(updatable);
    
    public void AddDrawableObject(IDrawable drawable)
        => drawableObjects.Add(drawable);
    
    public void DestroyUpdatableObject(IUpdatable updatable)
        => updatableObjects.RemoveSwap(updatable);
    
    public void DestroyDrawableObject(IDrawable drawable)
        => drawableObjects.RemoveSwap(drawable);
    
    public void LoadDataToGameLoop()
        => loop.SetData(updatableObjects, drawableObjects);

    public void Deactivate()
    {
        SceneRules.OnDelete();
        
        updatableObjects.Clear();
        drawableObjects.Clear();
    }
}