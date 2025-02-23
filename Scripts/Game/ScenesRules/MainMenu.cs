using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.Audio;
using Agario.Scripts.Game.Configurations;
using SFML.Window;

namespace Agario.Scripts.Game.ScenesRules;

public class MainMenu : ISceneRules
{
    private int currentSelectedSkin;
    private List<string> skinsNames;
    
    public void Init()
    {
        ServiceLocator.Instance.Register(new AudioSystem());
        GameConfig.SetData(new GameData());
    }
    
    public void Start()
    {
        skinsNames = new();
        Input.RegisterControllerKey(Keyboard.Key.Enter, StartGame, "Start", false);
    }

    private void StartGame()
    {
        SceneLoader.LoadScene("Game");
    }

    private void ScrollToRight()
        => NextSkin(1);
    
    private void ScrollToLeft()
        => NextSkin(-1);
    
    private void NextSkin(int bill)
    {
        int indesOfSkinToSelect = currentSelectedSkin + bill;
        
        if (indesOfSkinToSelect > skinsNames.Count || indesOfSkinToSelect < 0)
            return;

        currentSelectedSkin = indesOfSkinToSelect;
    }

    public void Update()
    {
        
    }
}