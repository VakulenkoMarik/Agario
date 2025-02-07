using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.Controllers;
using Agario.Scripts.Game.CustomExtensions;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game;

public class AgarioGame : IGameRules
{
    public static readonly List<Food> foodList = new();
    public static readonly List<Controller> controllersList = new();
    
    private static readonly List<GameObject> destructionList = new();

    public AgarioGame()
    {
        GameInit();
        
        ActivePlayerInit();
        
        AiPlayersInit();
    }

    private void GameInit()
    {
        GameConfig.SetData(new GameData());
    }
    
    private void ActivePlayerInit()
    {
        float posX = ProgramConfig.Data.WindowWidth / 2f;
        float posY = ProgramConfig.Data.WindowHeight / 2f;
        
        Player activePlayer = new(20f)
        {
            Position = new Vector2f(posX, posY)
        };

        Controller controller = new HumanController(activePlayer);
        
        controllersList.Add(controller);
    }

    private void AiPlayersInit()
    {
        int maxXPos = ProgramConfig.Data.WindowWidth;
        int maxYPos = ProgramConfig.Data.WindowHeight;
        
        for (int i = 0; i < GameConfig.Data.PlayersVolume - 1; i++)
        {
            Player player = new(Randomizer.Next(10, 30));
            
            int x = Randomizer.Next(0, maxXPos);
            int y = Randomizer.Next(0, maxYPos);
            
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
        if (foodList.Count < GameConfig.Data.FoodVolume)
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

    public static int ActivePlayersCount
    {
        get
        {
            int count = 0;
            
            foreach (Controller cont in controllersList)
            {
                if (cont.player is not null)
                {
                    count++;
                }
            }

            return count;
        }
    }
}