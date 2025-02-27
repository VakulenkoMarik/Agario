// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using SFML.Graphics;

namespace Agario.Scripts.Engine;

public class GameLoop(RenderWindow window)
{
    private List<IDrawable>? drawableObjects;
    private List<IUpdatable>? updatableObjects;
    
    private Color backgroundColor;

    private Action? onEndGameLoop;
    private bool isEndGameLoop;

    public void SetData(List<IUpdatable> updatables, List<IDrawable> drawables)
    {
        drawableObjects = drawables;
        updatableObjects = updatables;
    }

    public void AddEndSceneAction(Action action)
        => onEndGameLoop += action;
    
    private void Init()
    {
        isEndGameLoop = false;
        backgroundColor = Color.White;
    }

    public void Stop()
    {
        onEndGameLoop?.Invoke();
        onEndGameLoop = null;
        
        drawableObjects?.Clear();
        updatableObjects?.Clear();

        drawableObjects = null;
        updatableObjects = null;
        
        isEndGameLoop = true;
    }
    
    public void Run()
    {
        Init();
        
        while (!IsEndGameLoop())
        {
            Input();
            Update();
            Render();
        }
    }
    
    private void Input()
    {
        window.DispatchEvents();

        InputSystem.Input.UpdateKeyStatuses();
    }
    
    private void Update()
    {
        Time.Update();
        
        SceneLoader.CurrentScene?.Update();

        if (updatableObjects is not null)
        {
            foreach (IUpdatable objectToUpdate in updatableObjects.ToList())
            {
                objectToUpdate.Update();
            }
        }
        
        foreach (var key in InputSystem.Input.Keys.Values.ToList())
        {
            key.TryActivateEvent();
        }
    }
    
    private void Render()
    {
        window.Clear(backgroundColor);

        if (drawableObjects is not null)
        {
            foreach (IDrawable objectToDraw in drawableObjects)
            {
                Drawable? shapeToDraw = objectToDraw.GetMesh();

                if (shapeToDraw is null)
                {
                    objectToDraw.Draw();
                    continue;
                }
                
                window.Draw(shapeToDraw);
            }
        }

        window.Display();
    }

    private bool IsEndGameLoop()
    {
        return !window.IsOpen || isEndGameLoop;
    }
}