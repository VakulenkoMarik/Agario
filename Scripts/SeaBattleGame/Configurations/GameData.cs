using Agario.Scripts.Engine.Utils;
using Agario.Scripts.SeaBattleGame.Rules;
using Sini;

namespace Agario.Scripts.SeaBattleGame.Configurations;

public class GameData
{
    public Gamemode Gamemode { get; private set; }

    public int OnePlayerShipsCount { get; private set; }
    
    public void Load()
    {
        var ini = new IniFile(PathUtils.GetGameIniFile());

        Gamemode = ini.GetPrimitive("Game", "Gamemode", Gamemode.EvE);
        
        OnePlayerShipsCount = ini.GetInt("Game", "ShipsForOnePlayer", 1);
    }
}