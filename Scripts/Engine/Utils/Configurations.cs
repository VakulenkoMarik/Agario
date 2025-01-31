using SFML.Window;
using Sini;

namespace Agario.Scripts.Engine.Utils;

public class Configurations
{
    public static Configurations Root { get; private set; }
    
    public int WindowHeight;
    public int WindowWidth;
    
    public int FoodVolume;
    public int PlayersVolume;
    
    public static readonly Random Randomizer = new();
    
    public Keyboard.Key SwapPlayersControllersKey = Keyboard.Key.F;
    
    private static void SetRoot(Configurations config)
    {
        Root = config;
    }
    
    public static void Init()
    {
        var ini = new IniFile(PathUtils.GetIniFile());
        
        var config = new Configurations
        {
            WindowHeight = ini.GetInt("Window", "WindowHeight", 100),
            WindowWidth = ini.GetInt("Window", "WindowWidth", 100),
            
            FoodVolume = ini.GetInt("Game", "FoodVolume", 1),
            PlayersVolume = ini.GetInt("Game", "PlayersVolume", 1),
            
            SwapPlayersControllersKey = ini.GetPrimitive("Controls", "SwapKey", Keyboard.Key.F),
            PlayerMoveKeys = new ActivePlayerMoveKeys(
                ini.GetPrimitive("Controls", "KeyToUp", Keyboard.Key.W),
                ini.GetPrimitive("Controls", "KeyToDown", Keyboard.Key.S),
                ini.GetPrimitive("Controls", "KeyToRight", Keyboard.Key.D),
                ini.GetPrimitive("Controls", "KeyToLeft", Keyboard.Key.A)
            )
        };

        SetRoot(config);
    }
    
    public ActivePlayerMoveKeys PlayerMoveKeys = new(
        Keyboard.Key.W,
        Keyboard.Key.S,
        Keyboard.Key.D,
        Keyboard.Key.A
    );
}

public readonly record struct ActivePlayerMoveKeys(
    Keyboard.Key KeyToUp,
    Keyboard.Key KeyToDown,
    Keyboard.Key KeyToRight,
    Keyboard.Key KeyToLeft
);