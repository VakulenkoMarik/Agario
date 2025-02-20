// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.Engine;

public class GameLoop
{
    private static GameLoop instance = null!;
    
    private List<IDrawable>? drawableObjects;
    private List<IUpdatable>? updatableObjects;
    
    private Color backgroundColor;
    private readonly RenderWindow scene;

    private bool isEndGameLoop = false;
    
    public GameLoop()
    {
        instance = this;

        uint width = (uint)ProgramConfig.Data.WindowWidth;
        uint height = (uint)ProgramConfig.Data.WindowHeight;
        
        scene = new RenderWindow(new VideoMode(width, height), "Game window");
    }

    public void SetData(List<IUpdatable> updatables, List<IDrawable> drawables)
    {
        drawableObjects = drawables;
        updatableObjects = updatables;
    }
    
    private void Init()
    {
        AddEndLoopAction(scene.Close);
        
        backgroundColor = Color.White;
    }

    public void AddEndLoopAction(Action action)
    {
        scene.Closed += (_, _) => action();
    }

    public void Stop()
    {
        isEndGameLoop = true;
    }

    public static GameLoop GetInstance()
    {
        return instance;
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
        scene.DispatchEvents();

        InputSystem.Input.UpdateKeyStatuses();
    }
    
    private void Update()
    {
        Time.Update();
        
        foreach (IUpdatable objectToUpdate in updatableObjects.ToList())
        {
            objectToUpdate.Update();
        }
        
        foreach (var key in InputSystem.Input.Keys.Values)
        {
            key.TryActivateEvent();
        }
    }
    
    private void Render()
    {
        scene.Clear(backgroundColor);

        foreach (IDrawable objectToDraw in drawableObjects)
        {
            Drawable shapeToDraw = objectToDraw.GetMesh();
            scene.Draw(shapeToDraw);
        }

        scene.Display();
    }

    private bool IsEndGameLoop()
    {
        return !scene.IsOpen || isEndGameLoop;
    }
}