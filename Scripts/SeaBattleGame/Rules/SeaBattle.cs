#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.UI;
using Agario.Scripts.SeaBattleGame.Configurations;
using Agario.Scripts.SeaBattleGame.Controllers;

namespace Agario.Scripts.SeaBattleGame.Rules;

public enum Gamemode
{
    PvP,
    PvE,
    EvE,
}

public class SeaBattle : ISceneRules
{
    private Canvas canvas;
    private readonly GameData data = GameConfig.Data;

    private Controller controller1;
    private Controller controller2;
    
    private Controller attacker;
    private Controller target;
    
    public void Init()
    {
        GameConfig.Data.Load();
    }
    
    public void Start()
    {
        CreateObjects();
            
        GenerateMaps();
    }

    private void CreateObjects()
    {
        SetPlayers();
        
        attacker = controller1;
        target = controller2;
        
        canvas = new Canvas();
    }
    
    private void SetPlayers()
    {
        switch (data.Gamemode)
        {
            case Gamemode.EvE:
                (controller1, controller2) = (new AiController(), new AiController());
                break;
            
            case Gamemode.PvE:
                (controller1, controller2) = (new HumanController(), new AiController());
                break;
            
            case Gamemode.PvP:
                (controller1, controller2) = (new HumanController(), new HumanController());
                break;
        }
    }

    private void GenerateMaps()
    {
        controller1.TargetPlayer.GenerateMap(canvas, data.OnePlayerShipsCount);
        controller2.TargetPlayer.GenerateMap(canvas, data.OnePlayerShipsCount);
    }

    public void Update()
    {
        TryToChangePlayer();
        
        if (IsGameOver)
            OnEndGame();
    }

    private void TryToChangePlayer()
    {
        if (!attacker.Attacks)
        {
            (attacker, target) = (target, attacker);
            
            attacker.Attack();
        }
    }

    private bool IsGameOver
        => controller1.IsDefeat() || controller2.IsDefeat();

    private Controller? GetWinner()
    {
        if (controller1.IsDefeat() && !controller2.IsDefeat())
            return controller2;
        
        if (controller2.IsDefeat() && !controller1.IsDefeat())
            return controller1;
        
        return null;
    }

    private void OnEndGame()
    {
        
    }
}