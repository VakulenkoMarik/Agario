// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.Engine;

public class GameLoop
{
    private List<IDrawable>? drawableObjects;
    private List<IUpdatable>? updatableObjects;
    
    private Color backgroundColor;
    private readonly RenderWindow render;

    private bool isEndGameLoop;
    
    public GameLoop()
    {
        uint width = (uint)ProgramConfig.Data.WindowWidth;
        uint height = (uint)ProgramConfig.Data.WindowHeight;
        
        render = new RenderWindow(new VideoMode(width, height), "Game window");
    }

    public void SetData(List<IUpdatable> updatables, List<IDrawable> drawables)
    {
        drawableObjects = drawables;
        updatableObjects = updatables;
    }
    
    private void Init()
    {
        AddEndLoopAction(render.Close);
        
        backgroundColor = Color.White;
    }

    public void AddEndLoopAction(Action action)
        => render.Closed += (_, _) => action();

    public void Stop()
        => isEndGameLoop = true;
    
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
        render.DispatchEvents();

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
        render.Clear(backgroundColor);

        if (drawableObjects is not null)
        {
            foreach (IDrawable objectToDraw in drawableObjects)
            {
                Drawable shapeToDraw = objectToDraw.GetMesh();
                render.Draw(shapeToDraw);
            }
        }

        render.Display();
    }

    private bool IsEndGameLoop()
    {
        return !render.IsOpen || isEndGameLoop;
    }
}