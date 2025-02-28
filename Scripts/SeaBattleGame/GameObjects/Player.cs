using Agario.Scripts.Engine.UI;

namespace Agario.Scripts.SeaBattleGame.GameObjects;

public class Player(int ships)
{
    public int ShipsCount { get; private set; } = ships;

    public void GenerateMap(Canvas canvas, int ships)
    {
        
    }
}