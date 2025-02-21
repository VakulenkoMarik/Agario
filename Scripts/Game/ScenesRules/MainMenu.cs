using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using SFML.Window;

namespace Agario.Scripts.Game.ScenesRules;

public class MainMenu : ISceneRules
{
    public void Start()
    {
        Input.RegisterControllerKey(Keyboard.Key.Enter, () => SceneLoader.LoadScene("Game"), "Start", false);
    }
}