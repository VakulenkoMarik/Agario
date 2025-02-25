// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine.Utils;

public static class PathUtils
{
    private const string programIniFilePath = "Resources\\Files\\ProgramConf.ini";
    private const string gameIniFilePath = "Resources\\Files\\GameConf.ini";
    
    public static string Get(string path)
        => Path.Combine(AppContext.BaseDirectory, path);

    public static string GetProgramIniFile()
        => Get(programIniFilePath);

    public static string GetGameIniFile()
        => Get(gameIniFilePath);
}