using Agario.Scripts.Engine.Utils;
using Sini;

namespace Agario.Scripts.Engine;

public class ProgramData
{
    public int WindowHeight { get; private set; }
    public int WindowWidth { get; private set; }
    
    public void Load()
    {
        var ini = new IniFile(PathUtils.GetProgramIniFile());
        
        WindowHeight = ini.GetInt("Window", "WindowHeight", 100);
        WindowWidth = ini.GetInt("Window", "WindowWidth", 100);
    }
}