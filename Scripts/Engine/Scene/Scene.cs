using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Scene;

public class Scene(ISceneRules rules, RenderWindow window)
{
    public bool IsInited;
    
    private readonly List<IUpdatable> updatableObjects = new();
    private readonly List<IDrawable> drawableObjects = new();

    private GameLoop gameLoop;
    
    private Action? onEndGameLoop;

    public void Start()
    {
        gameLoop = new(window);
        
        if (!IsInited)
        {
            rules.Init();
            IsInited = true;
        }
        
        rules.Start();
        
        gameLoop.SetData(updatableObjects, drawableObjects);
        gameLoop.Run();
        
        Deactivate();
    }

    public void Stop()
        => gameLoop.Stop();

    public void AddOnExitSceneAction(Action action)
        => onEndGameLoop += action;
    
    public void Update()
        => rules.Update();
    
    public void AddUpdatableObject(IUpdatable updatable)
        => updatableObjects.Add(updatable);
    
    public void AddDrawableObject(IDrawable drawable)
        => drawableObjects.Add(drawable);
    
    public void DestroyUpdatableObject(IUpdatable updatable)
        => updatableObjects.RemoveSwap(updatable);
    
    public void DestroyDrawableObject(IDrawable drawable)
        => drawableObjects.RemoveSwap(drawable);

    private void Deactivate()
    {
        Input.Keys.Clear();
        rules.OnEnd();
        
        onEndGameLoop?.Invoke();
        onEndGameLoop = null;
        
        updatableObjects.Clear();
        drawableObjects.Clear();

        gameLoop = null;
    }
}