using Agario.Scripts.Engine.Utils;
using SFML.Window;
using Sini;

namespace Agario.Scripts.Game;

public static class GameData
{
    static GameData()
    {
        var ini = new IniFile(PathUtils.GetGameIniFile());

        FoodVolume = ini.GetInt("Game", "FoodVolume", 1);
        PlayersVolume = ini.GetInt("Game", "PlayersVolume", 1);

        SwapPlayersControllersKey = ini.GetPrimitive("Controls", "SwapKey", Keyboard.Key.F);

        PlayerMoveKeys = new ActivePlayerMoveKeys(
            ini.GetPrimitive("Controls", "KeyToUp", Keyboard.Key.W),
            ini.GetPrimitive("Controls", "KeyToDown", Keyboard.Key.S),
            ini.GetPrimitive("Controls", "KeyToRight", Keyboard.Key.D),
            ini.GetPrimitive("Controls", "KeyToLeft", Keyboard.Key.A)
        );
    }
    
    public static readonly int FoodVolume;
    public static readonly int PlayersVolume;
    
    public static readonly Keyboard.Key SwapPlayersControllersKey;
    
    public static ActivePlayerMoveKeys PlayerMoveKeys;
}

public readonly record struct ActivePlayerMoveKeys(
    Keyboard.Key KeyToUp,
    Keyboard.Key KeyToDown,
    Keyboard.Key KeyToRight,
    Keyboard.Key KeyToLeft
);