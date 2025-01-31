namespace Agario.Scripts.Engine.Utils;

public static class Configurations
{
    public static ProgramData Data { get; private set; }
    
    public static readonly Random Randomizer = new();

    static Configurations()
    {
        Data = new ProgramData();
    }
}