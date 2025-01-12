using Agario.Scripts.Engine;
using Agario.Scripts.Engine.ExtensionMethods;
using Agario.Scripts.GameProcess.GameObjects;
using SFML.Graphics;

namespace Agario.Scripts.GameProcess;

public class AgarioGame : Game
{
    public AgarioGame()
    {
        player = new Player(Color.Blue);
    }
    
    private int foodVolume = 20;
    public static List<Food> foodList = new();
    
    private Player player;

    public override void Update()
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

                DestroyAndRemoveFood(foodList[i]);
            }
        }
    }
    
    public void DestroyAndRemoveFood(Food food)
    {
        food.Destroy();

        foodList.Remove(food);
    }
}