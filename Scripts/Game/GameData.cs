// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Utils;
using SFML.Window;
using Sini;

namespace Agario.Scripts.Game;

public class GameData
{
    public int FoodVolume { get; private set; }
    public int PlayersVolume { get; private set; }
    
    public Keyboard.Key SwapPlayersControllersKey { get; private set; }
    public Keyboard.Key PauseKey { get; private set; }
    public Keyboard.Key Happy { get; private set; }
    
    public ActivePlayerMoveKeys PlayerMoveKeys { get; private set; }
    
    public void Load()
    {
        var ini = new IniFile(PathUtils.GetGameIniFile());

        FoodVolume = ini.GetInt("Game", "FoodVolume", 1);
        PlayersVolume = ini.GetInt("Game", "PlayersVolume", 1);

        SwapPlayersControllersKey = ini.GetPrimitive("Controls", "SwapPlayersControllersKey", Keyboard.Key.F);
        PauseKey = ini.GetPrimitive("Controls", "PauseKey", Keyboard.Key.F);
        Happy = ini.GetPrimitive("Controls", "Happy", Keyboard.Key.F);

        PlayerMoveKeys = new ActivePlayerMoveKeys(
            ini.GetPrimitive("Controls", "KeyToUp", Keyboard.Key.W),
            ini.GetPrimitive("Controls", "KeyToDown", Keyboard.Key.S),
            ini.GetPrimitive("Controls", "KeyToRight", Keyboard.Key.D),
            ini.GetPrimitive("Controls", "KeyToLeft", Keyboard.Key.A),
            ini.GetPrimitive("Controls", "KeyToRun", Keyboard.Key.Space)
        );
    }
}

public readonly record struct ActivePlayerMoveKeys(
    Keyboard.Key KeyToUp,
    Keyboard.Key KeyToDown,
    Keyboard.Key KeyToRight,
    Keyboard.Key KeyToLeft,
    Keyboard.Key KeyToRun
);