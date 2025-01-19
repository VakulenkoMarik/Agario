using SFML.Window;

namespace Agario.Scripts.Engine.Utils;

public static class Configurations
{
    public const int WindowHeight = 768;
    public const int WindowWidth = 1280;
    
    public static readonly Random Randomizer = new();

    public static readonly Keyboard.Key ActivePlayerKeyToUp = Keyboard.Key.W;
    public static readonly Keyboard.Key ActivePlayerKeyToDown = Keyboard.Key.S;
    public static readonly Keyboard.Key ActivePlayerKeyToRight = Keyboard.Key.D;
    public static readonly Keyboard.Key ActivePlayerKeyToLeft = Keyboard.Key.A;
}