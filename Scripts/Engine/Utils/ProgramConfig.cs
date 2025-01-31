namespace Agario.Scripts.Engine.Utils;

public static class ProgramConfig
{
    public static ProgramData Data { get; private set; }
    
    public static readonly Random Randomizer = new();

    static ProgramConfig()
    {
        Data = new ProgramData();
    }
}