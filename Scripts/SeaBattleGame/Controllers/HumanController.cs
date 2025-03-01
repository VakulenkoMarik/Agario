using Agario.Scripts.SeaBattleGame.GameObjects;
using TGUI;

namespace Agario.Scripts.SeaBattleGame.Controllers;

public class HumanController : Controller
{
    protected override void AddActionsToCells()
    {
        List<Cell> cells = TargetField.Grid.Cells;

        foreach (var value in cells)
        {
            Cell cell = value;
            Button button = value.TargetButton;
            
            button.Connect("Clicked", cell.GetShot);
        }
    }
}