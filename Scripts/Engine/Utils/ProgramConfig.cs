namespace Agario.Scripts.Engine.Utils;

public static class ProgramConfig
{
    public static ProgramData Data { get; private set; }

    static ProgramConfig()
    {
        Data = new ProgramData();
    }
}