using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.SeaBattleGame.Configurations;

namespace Agario.Scripts.SeaBattleGame.GameObjects;

public class Field : GameObject
{
    public int ShipsCount { get; private set; }
    public readonly CellsGrid Grid = new(GameConfig.Data.StandartFieldSize);

    public void SetCellsVisibility(bool isVisible)
    {
        foreach (var cell in Grid.Cells)
        {
            cell.SetVisibility(isVisible);
        }
    }

    public void PlaceShips(int ships)
    {
        ShipsCount = ships;

        if (Grid.CellsCount < ships)
            throw new InvalidOperationException("Not enough cells to place all ships!");

        List<Cell> keys = Grid.Cells;

        for (int i = 0; i < ships; i++)
        {
            int index = Randomizer.Next(0, keys.Count);
            keys[index].PlaceShip();
        }
    }
}