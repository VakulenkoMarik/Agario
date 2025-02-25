// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.Audio;
using Agario.Scripts.Game.Configurations;
using Agario.Scripts.Game.Controllers;
using Agario.Scripts.Game.CustomExtensions;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.ScenesRules;

public class AgarioGame : ISceneRules
{
    public static List<Food> foodList;
    public static List<Controller> controllersList;
    
    private static List<GameObject> destructionList;

    private PauseActivator pauseActivator => ServiceLocator.Instance.Get<PauseActivator>();

    public void Start()
    {
        GameInit();
        
        ActivePlayerInit();
        
        AiPlayersInit();
    }

    private void GameInit()
    {
        foodList = new();
        controllersList = new();
        destructionList = new();
        
        // Input
        PauseActivator activator = ServiceLocator.Instance.Get<PauseActivator>();
        Input.RegisterControllerKey(GameConfig.Data.PauseKey, activator.PauseToggle, "pause", false);
        
        // Audio system
        AudioSystem system = ServiceLocator.Instance.Get<AudioSystem>();
        system.Play(AudioType.Background);
        system.SetVolume("Background", 20);
    }
    
    private void ActivePlayerInit()
    {
        float posX = ProgramConfig.Data.WindowWidth / 2f;
        float posY = ProgramConfig.Data.WindowHeight / 2f;
        
        GameCharactersList list = ServiceLocator.Instance.Get<GameCharactersList>();
        GameCharacter? character = list.TryGetNewCharacter("Boba");

        if (character is not null)
        {
            Player activePlayer = new()
            {
                Position = new Vector2f(posX, posY)
            };
        
            activePlayer.ShapeInit(30);
            activePlayer.SetCharacter(character);

            Controller controller = new HumanController(activePlayer);
        
            controllersList.Add(controller);
        }
    }

    private void AiPlayersInit()
    {
        int maxXPos = ProgramConfig.Data.WindowWidth;
        int maxYPos = ProgramConfig.Data.WindowHeight;

        GameCharactersList list = ServiceLocator.Instance.Get<GameCharactersList>();
        
        for (int i = 0; i < GameConfig.Data.PlayersVolume - 1; i++)
        {
            GameCharacter? character = list.TryGetNewCharacter("Boba");
            
            if (character is not null)
            {
                Player player = new();
                player.ShapeInit(Randomizer.Next(10, 30));
                player.SetCharacter(character);
            
                int x = Randomizer.Next(0, maxXPos);
                int y = Randomizer.Next(0, maxYPos);
            
                player.Position = new Vector2f(x, y);
            
                Controller controller = new BotController(player);
            
                controllersList.Add(controller);
            }
        }
    }

    public void Update()
    {
        if (!pauseActivator.IsPause)
        {
            ClearDeathList();

            StartOfNewObjects();
        
            CollisionsHandling();
            
            if (ActivePlayersCount == 1)
                OnEndGame();
        }
    }
    
    private void OnEndGame()
        => SceneLoader.LoadScene("MainMenu");

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
                controllersList.RemoveSwap(botController);
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