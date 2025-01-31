using Agario.Scripts.Engine.Utils;
using Sini;

namespace Agario.Scripts.Engine;

public class ProgramData
{
    public readonly int WindowHeight;
    public readonly int WindowWidth;
    
    public ProgramData()
    {
        var ini = new IniFile(PathUtils.GetProgramIniFile());
        
        WindowHeight = ini.GetInt("Window", "WindowHeight", 100);
        WindowWidth = ini.GetInt("Window", "WindowWidth", 100);
    }
    
}