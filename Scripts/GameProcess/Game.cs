using Agario.Scripts.Engine;
using Agario.Scripts.Engine.ExtensionMethods;
using Agario.Scripts.Engine.Settings;
using Agario.Scripts.GameProcess.GameObjects;
using SFML.Graphics;
using SFML.Window;

namespace Agario.Scripts.GameProcess;

public class Game
{
    public RenderWindow Scene { get; private set; }
    public static List<Food> foodList = new();

    private GameLoop gameLoop;
    private Player player;

    private int foodVolume = 20;

    public Game()
    {
        Scene = new RenderWindow(new VideoMode(Configurations.WindowWidth, Configurations.WindowHeight), "Game window");
        Scene.Closed += (sender, e) => Scene.Close();

        gameLoop = new GameLoop(this);
        player = new Player(Color.Blue);
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
