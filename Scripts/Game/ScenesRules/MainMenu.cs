using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using Agario.Scripts.Engine.UI;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.Audio;
using Agario.Scripts.Game.Configurations;
using SFML.Graphics;
using TGUI;
using Color = SFML.Graphics.Color;

namespace Agario.Scripts.Game.ScenesRules;

public class MainMenu : ISceneRules
{
    private int currentCharacterIndex;
    private readonly float currentCharacterRadius = 200;
    
    private List<GameCharacter> skins;
    
    private Canvas mainMenuCanvas;

    private LobbyCharacter lobbyCharacter;
    public class LobbyCharacter(CircleShape shape) : IDrawable
    {
        public Drawable GetMesh()
            => shape;
    }
    
    public void Init()
    {
        ServiceLocator.Instance.Register(new AudioSystem());
        ServiceLocator.Instance.Register(new GameCharactersList());
        ServiceLocator.Instance.Register(new AnimationsList());
        
        GameConfig.SetData(new GameData());
    }
    
    public void Start()
    {
        skins = ServiceLocator.Instance.Get<GameCharactersList>().Characters;
        
        mainMenuCanvas = new Canvas();
        
        CurrentSkinInit();
        
        SceneLoader.CurrentScene?.AddDrawableObject(lobbyCharacter);

        UiInit();
    }

    private void UiInit()
    {
        ChangeSelectedCharacter();
        
        Vector2f pos = new Vector2f(ProgramConfig.Data.WindowWidth / 2, ProgramConfig.Data.WindowHeight - 150);
        Vector2f size = new Vector2f(200, 100);
        Button playButton = UiFactory.CreateButton(pos, size, StartGame, "PLAY");
        
        Vector2f pos2 = new Vector2f(ProgramConfig.Data.WindowWidth - 100, ProgramConfig.Data.WindowHeight / 2);
        Vector2f size2 = new Vector2f(100, 200);
        Button buttonToRight = UiFactory.CreateButton(pos2, size2, ScrollToRight, "\u21a6");
        buttonToRight.TextSize = 100;
        
        Vector2f pos3 = new Vector2f(100, ProgramConfig.Data.WindowHeight / 2);
        Vector2f size3 = new Vector2f(100, 200);
        Button buttonToLeft = UiFactory.CreateButton(pos3, size3, ScrollToLeft, "\u21a4");
        buttonToLeft.TextSize = 100;
        
        mainMenuCanvas.AddWidget(playButton, "playButton");
        mainMenuCanvas.AddWidget(buttonToRight, "playButton");
        mainMenuCanvas.AddWidget(buttonToLeft, "playButton");
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
        int indesOfSkinToSelect = currentCharacterIndex + bill;
        
        if (indesOfSkinToSelect > skins.Count || indesOfSkinToSelect < 0)
            return;

        currentCharacterIndex = indesOfSkinToSelect;

        ChangeSelectedCharacter();
    }

    private void CurrentSkinInit()
    {
        CircleShape shape = new();
        shape.Radius = currentCharacterRadius;
        shape.Origin = new SFML.System.Vector2f(shape.Radius, shape.Radius);
        shape.Position = new SFML.System.Vector2f(ProgramConfig.Data.WindowWidth / 2, 250);
        
        lobbyCharacter = new(shape);
    }

    private void ChangeSelectedCharacter()
    {
        //currentCharacter.Texture = skins[currentCharacterIndex].IconTexture;
        Color color = new();
        CircleShape shape = lobbyCharacter.GetMesh() as CircleShape;
        shape.FillColor = color.GenerateColor(10, 200);
        
        Console.WriteLine(111);
        Console.WriteLine(shape.FillColor);
    }
}