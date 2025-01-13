using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.Engine;

public class GameLoop
{
    private static GameLoop instance;
    
    public List<IDrawable> drawableObjects = new();
    public List<IUpdatable> updatableObjects = new();
    
    private Color backgroundColor;
    private RenderWindow scene;

    private bool isEndGameLoop;
    
    public GameLoop()
    {
        scene = new RenderWindow(new VideoMode(Configurations.WindowWidth, Configurations.WindowHeight), "Game window");
        scene.Closed += (sender, e) => scene.Close();
        
        backgroundColor = Color.White;

        instance = this;
    }

    public static GameLoop GetInstance()
    {
        return instance;
    }
    
    public void Run()
    {
        isEndGameLoop = false;
        
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
    
    private void CheckInput()
    {
        scene.DispatchEvents();
    }
    
    private void Update()
    {
        Time.Update();
        
        foreach (IUpdatable objectToUpdate in updatableObjects)
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