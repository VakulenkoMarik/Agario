using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.Controllers;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game;

public class AgarioGame : IGameRules
{
    private readonly Random random = Configurations.Randomizer;
    
    public static readonly List<Food> foodList = new();
    public static readonly List<Controller> controllersList = new();
    
    private static readonly List<GameObject> destructionList = new();

    private readonly int foodVolume = 50;
    private readonly int playersVolume = 5;

    public AgarioGame()
    {
        ActivePlayerInit();
        
        AiPlayersInit();
    }

    private void ActivePlayerInit()
    {
        float posX = Configurations.WindowWidth / 2f;
        float posY = Configurations.WindowHeight / 2f;
        
        Player activePlayer = new(20f)
        {
            Position = new Vector2f(posX, posY)
        };

        Controller controller = new HumanController(activePlayer);
        
        controllersList.Add(controller);
    }

    private void AiPlayersInit()
    {
        int maxXPos = Configurations.WindowWidth;
        int maxYPos = Configurations.WindowHeight;
        
        for (int i = 0; i < playersVolume; i++)
        {
            Player player = new(Configurations.Randomizer.Next(10, 30));
            
            int x = random.Next(0, maxXPos);
            int y = random.Next(0, maxYPos);
            
            player.Position = new Vector2f(x, y);
            
            Controller controller = new BotController(player);
            
            controllersList.Add(controller);
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
        foreach (Controller cont in controllersList)
        {
            if (cont.player == null)
                continue;
            
            HandlePlayerFoodCollisions(cont.player);
        }
    }
    
    private void HandlePlayerFoodCollisions(Player player)
    {
        foreach (Food food in foodList)
        {
            player.FoodLunchAttempt(food);
        }
    }

    private void PlayersCollisionHandling()
    {
        foreach (Controller cont in controllersList)
        {
            if (cont.player == null)
                continue;
            
            HandlePlayerPlayerCollisions(cont.player, controllersList);
        }
    }
    
    private void HandlePlayerPlayerCollisions(Player attacker, List<Controller> targetsList)
    {
        foreach (Controller target in targetsList)
        {
            Player? player = target.player;
            
            if (player is null || attacker.Radius <= player.Radius)
            {
                continue;
            }

            attacker.PlayerLunchAttempt(target);
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
        if (gameObject is Controller controller)
        {
            if (controller is BotController botController)
            {
                botController.Delete();
                return;
            }

            controller.player = null;
        }
        else if (gameObject is Food food)
        {
            food.NewFood();
        }
    }

    public static void AddToDeathList(GameObject victim)
    {
        destructionList.Add(victim);
    }
}