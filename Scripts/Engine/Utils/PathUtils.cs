// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine.Utils;

public static class PathUtils
{
    private const string programIniFilePath = "Files\\ProgramConf.ini";
    private const string gameIniFilePath = "Files\\GameConf.ini";
    
    public static string Get(string path)
    {
        return Path.Combine(AppContext.BaseDirectory, path);
    }

    public static string GetProgramIniFile()
    {
        return Get(programIniFilePath);
    }

    public static string GetGameIniFile()
    {
        return Get(gameIniFilePath);
    }
}