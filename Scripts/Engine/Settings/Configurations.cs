namespace Agario.Scripts.Engine.Settings;

public static class Configurations
{
    public const int WindowHeight = 768;
    public const int WindowWidth = 1280;
    
    public static Random Random { get; private set; } = new();
}