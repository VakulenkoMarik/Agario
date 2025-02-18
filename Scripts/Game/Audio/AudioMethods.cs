// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Audio;

namespace Agario.Scripts.Game.Audio;

public readonly struct AudioMethods(AudioPlayer audioPlayer)
{
    public void Movement()
        => audioPlayer.PlayIfNotPlaying("Movement");
    
    public void Running()
        => audioPlayer.Play("Running");
    
    public void Happy()
        => audioPlayer.Play("Happy");
    
    public void Background()
        => audioPlayer.Play("Background", false);
    
    public void SomeoneWasKilled()
    {
        if (AgarioGame.ActivePlayersCount == GameConfig.Data.PlayersVolume)
        {
            audioPlayer.Play("FirstKill");
        }

        if (AgarioGame.ActivePlayersCount == 2)
        {
            audioPlayer.Play("TheLastSurvivor");
            audioPlayer.TryGet("Background")?.Stop();
        }
            
        audioPlayer.Play("Bite");
    }
}