using Agario.Scripts.Engine;
using Agario.Scripts.Engine.ExtensionMethods;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using Agario.Scripts.GameProcess.GameObjects;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.GameProcess;

public class Game : IUpdatable
{
    public RenderWindow render;
    public static List<Food> foodList = new();

    private GameLoop gameLoop;
    private Player player;

    private int foodVolume = 20;

    public Game()
    {
        render = new RenderWindow(new VideoMode(Configurations.WindowWidth, Configurations.WindowHeight), "Game window");
        render.Closed += (sender, e) => render.Close();

        gameLoop = new GameLoop(render);
        player = new Player(Color.Blue);
        
        GameLoop.updatableObjects.Add(this);
    }

    public void Start()
    {
        gameLoop.Run();
    }

    public void Update()
    {
        TryGenerateFood();
        CollisionsHandling();
    }

    private void TryGenerateFood()
    {
        if (foodList.Count < foodVolume)
        {
            Color foodFillColor = new Color();
            foodFillColor = foodFillColor.GenerateColor(10, 255);

            Food food = new Food(foodFillColor);
            foodList.Add(food);

            food.PutOnMap();
        }
    }

    private void CollisionsHandling()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (player.CollidesWith(foodList[i]))
            {
                player.Grow(foodList[i].Kilo);
                foodList[i].Destroy();
            }
        }
    }
}
