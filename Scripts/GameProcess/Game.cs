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
    public List<Food> foodList = new();

    private GameLoop gameLoop;

    private int foodVolume = 20;

    public Game()
    {
        Scene = new RenderWindow(new VideoMode(Configurations.WindowWidth, Configurations.WindowHeight), "Game window");
        Scene.Closed += (sender, e) => Scene.Close();

        gameLoop = new GameLoop(this);
    }

    public void Start()
    {
        gameLoop.Run();
    }

    public void Update()
    {
        TryGenerateFood();
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
}
