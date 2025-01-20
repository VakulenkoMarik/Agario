using SFML.Window;

namespace Agario.Scripts.Engine.Utils;

public static class Configurations
{
    public const int WindowHeight = 768;
    public const int WindowWidth = 1280;

    public static readonly Random Randomizer = new();

    public static readonly ActivePlayerKeys PlayerKeys = new(
        Keyboard.Key.W,
        Keyboard.Key.S,
        Keyboard.Key.D,
        Keyboard.Key.A
    );
}

public readonly record struct ActivePlayerKeys(
    Keyboard.Key KeyToUp,
    Keyboard.Key KeyToDown,
    Keyboard.Key KeyToRight,
    Keyboard.Key KeyToLeft
);