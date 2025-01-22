using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.GameObjects;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game;

public class AgarioGame : IGameRules
{
    private readonly Random random = Configurations.Randomizer;
    
    public static readonly List<Food> foodList = new();
    public static readonly List<Player> playersList = new();
    
    private readonly List<GameObject> destructionList = new();

    private readonly int foodVolume = 50;
    private readonly int playersVolume = 5;

    public AgarioGame()
    {
        var activePlayer = new Player(20f);
        playersList.Add(activePlayer);

        AiPlayersInit();
    }

    private void AiPlayersInit()
    {
        int maxXPos = Configurations.WindowWidth;
        int maxYPos = Configurations.WindowHeight;
        
        for (int i = 0; i < playersVolume; i++)
        {
            Player player = new Bot();
            playersList.Add(player);

            int x = random.Next(0, maxXPos);
            int y = random.Next(0, maxYPos);
            
            player.Drop(x, y);
        }
    }

    public void Update()
    {
        ClearDeathList();

        StartOfNewObjects();
        
        CollisionsHandling();
    }

    private void StartOfNewObjects()
    {
        TryGenerateFood();
    }

    private void TryGenerateFood()
    {
        if (foodList.Count < foodVolume)
        {
            Food food = new Food();
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
        foreach (Player player in playersList)
        {
            HandlePlayerFoodCollisions(player);
        }
    }
    
    private void HandlePlayerFoodCollisions(Player player)
    {
        foreach (Food food in foodList)
        {
            if (player.CollidesWith(food))
            {
                player.Grow(food);
                destructionList.Add(food);
            }
        }
    }

    private void PlayersCollisionHandling()
    {
        foreach (Player attacker in playersList)
        {
            HandlePlayerPlayerCollisions(attacker, playersList);
        }
    }
    
    private void HandlePlayerPlayerCollisions(Player attacker, List<Player> targetsList)
    {
        foreach (Player target in targetsList)
        {
            if (attacker.Radius <= target.Radius)
            {
                continue;
            }
                
            if (attacker.CollidesWith(target))
            {
                attacker.Grow(target);
                destructionList.Add(target);
            }
        }
    }

    private void ClearDeathList()
    {
        foreach (GameObject ghost in destructionList)
        {
            RecyclingObject(ghost);
        }
        
        destructionList.Clear();
    }

    private void RecyclingObject(GameObject gameObject)
    {
        if (gameObject is Player player)
        {
            playersList.RemoveSwap(player);
        }
        else if (gameObject is Food food)
        {
            food.NewFood();
        }
    }
}