#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.UI;
using Agario.Scripts.SeaBattleGame.Configurations;
using Agario.Scripts.SeaBattleGame.GameObjects;
using Agario.Scripts.SeaBattleGame.Rules;
using TGUI;

namespace Agario.Scripts.SeaBattleGame.Controllers;

public class Controller : GameObject
{
    private readonly GameData data = GameConfig.Data;
    public bool Attacks;

    public Field TargetField { get; private set; } = new();

    public void SetField(Field field)
        => TargetField = field;

    public void Attack()
    {
        Attacks = true;
        
        AttackProcessing();
    }

    public void GenerateGridMap(Canvas canvas, Vector2f offset)
    {
        bool showShips = this switch
        {
            HumanController => true,
            AiController => false,
            _ => false
        };

        if (GameConfig.Data.Gamemode is Gamemode.PvP)
            showShips = !showShips;
        
        TargetField.Grid.CreateMapGrid(data.StandartCellSize, showShips, offset, canvas);
        TargetField.PlaceShips(data.OnePlayerShipsCount);
        
        AddActionsToCells();
    }
    
    protected virtual void AddActionsToCells() { }
    
    protected virtual void AttackProcessing() { }

    protected void EndAttack()
        => Attacks = false;

    public bool IsDefeat()
        => TargetField.ShipsCount <= 0;
}