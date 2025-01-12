using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine;

public class GameLoop
{
    public static List<IDrawable> drawableObjects = new();
    public static List<IUpdatable> updatableObjects = new();
    
    private RenderWindow scene;
    private Color backgroundColor;
    
    public GameLoop(RenderWindow scene)
    {
        backgroundColor = Color.White;
        
        this.scene = scene;
    }
    
    public void Run()
    {
        while (scene.IsOpen)
        {
            CheckInput();
            Update();
            Render();
        }
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
            Shape shapeToDraw = objectToDraw.GetShape();
            scene.Draw(shapeToDraw);
        }

        scene.Display();
    }
    
    private void CheckInput()
    {
        scene.DispatchEvents();
    }
}