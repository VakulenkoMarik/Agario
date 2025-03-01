// ReSharper disable InconsistentNaming

using TGUI;

namespace Agario.Scripts.SeaBattleGame.GameObjects;

public enum CellStatus
{
    Hit,
    Miss,
    Ship,
    NotDefined,
}

public class Cell
{
    private static int cellID;
    public int ID { get; private set; }

    public readonly Button TargetButton;

    public Cell(Button button)
    {
        cellID++;
        ID = cellID;

        TargetButton = button;
    }
    
    private bool isShot = false;
    private bool isShip = false;
    private bool isVisible = false;

    public void GetShot()
    {
        isShot = true;
        
        SetColor();
    }
    
    public bool ContainsShip()
        => isShip;

    public void PlaceShip()
        => isShip = true;

    public void SetVisibility(bool visibility)
        => isVisible = visibility;

    private void SetColor()
    {
        
    }

    public CellStatus Status
    {
        get
        {
            return (isShip, isShot, isVisible) switch
            {
                (true, true, _) => CellStatus.Hit,
                (false, true, _) => CellStatus.Miss,
                (true, false, true) => CellStatus.Ship,
                _ => CellStatus.NotDefined
            };
        }
    }
}