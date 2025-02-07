using Agario.Scripts.Engine.Audio;
using Agario.Scripts.Engine.Interfaces;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class Game : IUpdatable
{
    private readonly GameLoop gameLoop;
    private IGameRules gameRules = null!;

    public Game()
    {
        FilesInit();
        
        gameLoop = new();
        
        gameLoop.updatableObjects.Add(this);

        AudioInit();
    }

    private void FilesInit()
    {
        ProgramConfig.Init();
    }
    
    private void AudioInit()
    {
        AudioPlayer.SetInstance(new AudioPlayer());
        AudioPlayer.Instance.Init();
    }

    public void SetGameRules(IGameRules rules)
    {
        gameRules = rules;
    }

    public void Start()
    {
        gameLoop.Run();
    }

    public void Update()
    {
        gameRules.Update();
    }
}
