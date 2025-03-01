using Agario.Scripts.Engine.Utils;
using Agario.Scripts.SeaBattleGame.Rules;
using SFML.Graphics;
using Sini;

namespace Agario.Scripts.SeaBattleGame.Configurations;

public class GameData
{
    public GameData()
    {
        Load();
    }
    
    public Gamemode Gamemode { get; private set; }
    public int OnePlayerShipsCount { get; private set; }
    public int StandartCellSize { get; private set; }
    public int StandartFieldSize { get; private set; }
    
    public CellColors ColorsForCell { get; private set; }
    
    public void Load()
    {
        var ini = new IniFile(PathUtils.GetGameIniFile());

        Gamemode = ini.GetPrimitive("Game", "Gamemode", Gamemode.EvE);
        
        OnePlayerShipsCount = ini.GetInt("Game", "ShipsForOnePlayer", 1);
        StandartCellSize = ini.GetInt("Game", "CellSize", 1);
        StandartFieldSize = ini.GetInt("Game", "FieldSize", 1);

        ColorsForCell = new CellColors(
            ini.GetPrimitive("Cell", "HitColor", Color.Red),
            ini.GetPrimitive("Cell", "MissColor", Color.Blue),
            ini.GetPrimitive("Cell", "ShipColor", Color.Green),
            ini.GetPrimitive("Cell", "NotDefinedColor", Color.White)
            );
    }
}

public readonly record struct CellColors(
    Color Hit,
    Color Miss,
    Color Ship,
    Color NotDefined
);