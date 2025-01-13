using Agario.Scripts.Engine;
using Agario.Scripts.Engine.ExtensionMethods;
using Agario.Scripts.Engine.Settings;
using Agario.Scripts.GameProcess.GameObjects;
using SFML.Graphics;

namespace Agario.Scripts.GameProcess;

public class AgarioGame : Game
{
    private Random random = Configurations.Random;
    
    private List<Food> foodList = new();
    private List<Player> playersList = new();
    private List<Player> deathList = new();

    private Player activePlayer;
    
    private int foodVolume = 50;
    private int playersVolume = 5;

    public AgarioGame()
    {
        activePlayer = new Player(Color.Blue, 35f);
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
            
            Player player = new Bot(fillColor);
            playersList.Add(player);

            int x = random.Next(0, maxXPos);
            int y = random.Next(0, maxYPos);
            
            player.Drop(x, y);
        }
    }

    public override void Update()
    {
        TryGenerateFood();
        
        BotsMovement();
        
        CollisionsHandling();

        ClearDeathList();
    }

    private void BotsMovement()
    {
        foreach (Player player in playersList)
        {
            if (player is Bot bot)
            {
                bot.MakeDecision(foodList, playersList);
            }
        }
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
        FoodCollisionHandling();
        PlayersCollisionHandling();
    }

    private void FoodCollisionHandling()
    {
        if (playersList.Count <= 0)
        {
            return;
        }
        
        foreach (Player player in playersList)
        {
            HandlePlayerFoodCollisions(player);
        }
    }
    
    private void HandlePlayerFoodCollisions(Player player)
    {
        for (int foodID = 0; foodID < foodList.Count; foodID++)
        {
            Food food = foodList[foodID];

            if (player.CollidesWith(food))
            {
                player.Grow(food);
                DismissObject(food);
            }
        }
    }

    private void PlayersCollisionHandling()
    {
        if (playersList.Count <= 0)
        {
            return;
        }
        
        List<Player> targetList = new List<Player>(playersList);

        foreach (Player player1 in playersList)
        {
            HandlePlayerCollisions(player1, targetList);

            targetList.Remove(player1);
        }
    }
    
    private void HandlePlayerCollisions(Player player1, List<Player> targetList)
    {
        foreach (Player player2 in targetList)
        {
            if (player1.CollidesWith(player2))
            {
                HandleCollisionResult(player1, player2);
            }
        }
    }
    
    private void HandleCollisionResult(Player player1, Player player2)
    {
        (Player? smaller, Player? bigger) = WhoBigger(player1, player2);

        if (smaller is not null && bigger is not null)
        {
            bigger.Grow(smaller);
            deathList.Add(smaller);
        }
    }

    private void ClearDeathList()
    {
        foreach (Player ghost in deathList)
        {
            DismissObject(ghost);
        }
    }

    private (Player? smaller, Player? bigger) WhoBigger(Player p1, Player p2)
    {
        if (p1.Radius > p2.Radius)
        {
            return (p2, p1);
        }
        
        if (p1.Radius < p2.Radius)
        {
            return (p1, p2);
        }

        return (null, null);
    }
    
    public void DismissObject(GameObject gameObject)
    {
        if (gameObject is Player)
        {
            playersList.Remove((Player)gameObject);
        }
        else if (gameObject is Food)
        {
            foodList.Remove((Food)gameObject);
        }
    }
}