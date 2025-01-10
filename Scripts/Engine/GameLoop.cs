using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using Agario.Scripts.GameProcess;
using SFML.Graphics;
using SFML.Window; 

namespace Agario.Scripts.Engine;

public class GameLoop
{
    public static List<IDrawable> drawableObjects = new();
    public static List<IUpdatable> updatableObjects = new();
    
    private RenderWindow scene;
    private Game game;
    private Color backgroundColor;
    
    public GameLoop(Game newGame)
    {
        backgroundColor = Color.White;
        
        game = newGame;
        scene = newGame.Scene;
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
        game.Update();
        
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