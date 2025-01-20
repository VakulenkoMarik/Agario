using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;
using SFML.Window;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class GameLoop
{
    private static GameLoop instance = null!;
    
    public readonly List<IDrawable> drawableObjects = new();
    public readonly List<IUpdatable> updatableObjects = new();
    public readonly List<IInputHandler> inputHandlerObjects = new();
    
    private readonly Color backgroundColor;
    private readonly RenderWindow scene;

    private bool isEndGameLoop;
    
    public GameLoop()
    {
        instance = this;
        
        scene = new RenderWindow(new VideoMode(Configurations.WindowWidth, Configurations.WindowHeight), "Game window");
        scene.Closed += (_, _) => scene.Close();
        
        backgroundColor = Color.White;
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
            CheckInput();
            Update();
            Render();
        }
    }

    public void Stop()
    {
        isEndGameLoop = true;
    }

    private void Init()
    {
        isEndGameLoop = false;
    }
    
    private void CheckInput()
    {
        scene.DispatchEvents();

        foreach (IInputHandler handler in inputHandlerObjects)
        {
            handler.HandleInput();
        }
    }
    
    private void Update()
    {
        Time.Update();
        
        foreach (IUpdatable objectToUpdate in updatableObjects.ToList())
        {
            objectToUpdate.Update();
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