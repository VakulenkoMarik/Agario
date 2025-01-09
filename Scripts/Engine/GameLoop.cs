using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.GameProcess;
using SFML.Graphics;
using SFML.Window; 

namespace Agario.Scripts.Engine;

public class GameLoop
{
    private static List<IDrawable> drawableObjects = new List<IDrawable>();
    
    private RenderWindow scene;
    private Game game;
    
    public GameLoop(Game newGame)
    {
        game = newGame;
        
        scene = new RenderWindow(new VideoMode(1920, 1080), "Game window");
        scene.Closed += (sender, e) => scene.Close();
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
    }
    
    private void Render()
    {
        scene.Clear(Color.White);

        foreach (IDrawable objectToDraw in drawableObjects)
        {
            objectToDraw.Draw();
        }

        scene.Display();
    }
    
    private void CheckInput()
    {
        scene.DispatchEvents();
    }
}