using Agario.Scripts.Engine.UI;
using TGUI;

namespace Agario.Scripts.SeaBattleGame.GameObjects;

public class CellsGrid(float fieldSize)
{
    public readonly List<Cell> Cells = new();
    
    private const float Space = 5.0f;
    private float cellSize;

    public int CellsCount
        => Cells.Count;

    public void CreateMapGrid(int relativeCellSize, bool isVisibleCells, Vector2f offset, Canvas canvas)
    {
        if (relativeCellSize <= 0)
            throw new ArgumentException("Grid size must be greater than zero.");

        cellSize = CalculateCellSize(relativeCellSize);

        GenerateCells(offset, isVisibleCells, canvas);
    }

    private float CalculateCellSize(int size)
    {
        float cellWidth = (fieldSize / 2 - (size + 1) * Space) / size;
        float cellHeight = (fieldSize - (size + 1) * Space) / size;
        return Math.Min(cellWidth, cellHeight);
    }

    private void GenerateCells(Vector2f startPos, bool isVisibleCells, Canvas canvas)
    {
        for (int y = 0; y < cellSize; y++)
        {
            for (int x = 0; x < cellSize; x++)
            {
                float posX = startPos.X + x * (cellSize + Space);
                float posY = startPos.Y + y * (cellSize + Space);

                Button button = CreateButton(new Vector2f(posX, posY));
                
                Cell cell = new Cell(button);
                cell.SetVisibility(isVisibleCells);
                
                canvas.AddWidget(button, $"Cell button: {cell.ID}");
                
                Cells.Add(cell);
            }
        }
    }

    private Button CreateButton(Vector2f pos)
    {
        Button button = new Button();
        
        button.SetSize(new Vector2f(cellSize, cellSize));
        button.SetPosition(pos);

        return button;
    }
}