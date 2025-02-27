using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;

namespace Agario.Scripts.Engine.Scene;

public class Scene(ISceneRules rules)
{
    public bool IsInited;
    
    private List<IUpdatable> updatableObjects = new();
    private List<IDrawable> drawableObjects = new();

    private ISceneRules SceneRules { get; set; } = rules;

    public (List<IUpdatable>, List<IDrawable>) GetData()
        => (updatableObjects, drawableObjects);

    public void Start()
    {
        if (!IsInited)
        {
            SceneRules.Init();
            IsInited = true;
        }
        
        SceneRules.Start();
    }

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

    public void Deactivate()
    {
        SceneRules.OnEnd();
        
        updatableObjects.Clear();
        drawableObjects.Clear();
    }
}