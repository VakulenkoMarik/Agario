using Agario.Scripts.Engine;
using Agario.Scripts.Engine.ExtensionMethods;
using Agario.Scripts.Engine.Settings;
using Agario.Scripts.GameProcess.GameObjects;
using SFML.Graphics;

namespace Agario.Scripts.GameProcess;

public class AgarioGame : Game
{
    private Random random = new();
    
    private List<Food> foodList = new();
    private List<Player> playersList = new();

    private Player activePlayer;
    
    private int foodVolume = 50;
    private int playersVolume = 5;

    public AgarioGame()
    {
        activePlayer = new Player(Color.Blue, true);
        playersList.Add(activePlayer);

        AIPlayersInit();
    }

    private void AIPlayersInit()
    {
        int maxXPos = Configurations.WindowWidth;
        int maxYPos = Configurations.WindowHeight;
        
        for (int i = 0; i < playersVolume; i++)
        {
            Color fillColor = new Color().GenerateColor(10, 255);
            
            Player player = new Player(fillColor, false);
            playersList.Add(player);

            int x = random.Next(0, maxXPos);
            int y = random.Next(0, maxYPos);
            
            player.Drop(x, y);
        }
    }

    public override void Update()
    {
        TryGenerateFood();
        CollisionsHandling();
    }
    
    private void TryGenerateFood()
    {
        if (foodList.Count < foodVolume)
        {
            Color foodFillColor = new Color().GenerateColor(10, 255);

            Food food = new Food(foodFillColor);
            foodList.Add(food);

            food.PutOnMap();
        }
    }
    
    private void CollisionsHandling()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (activePlayer.CollidesWith(foodList[i]))
            {
                activePlayer.Grow(foodList[i].Kilo);

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