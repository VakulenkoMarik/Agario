// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine.Data;

public static class ProgramConfig
{
    public static ProgramData Data { get; private set; } = null!;

    private static bool isInitialised = false;

    public static void Init()
    {
        if (!isInitialised)
        {
            Data = new ProgramData();
            Data.Load();

            isInitialised = true;
        }
    }
}