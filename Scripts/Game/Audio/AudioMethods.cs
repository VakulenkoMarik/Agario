// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Audio;
using SFML.System;

namespace Agario.Scripts.Game.Audio;

public readonly struct AudioMethods(AudioPlayer audioPlayer)
{
    public void Movement(Vector2f deltaDirection)
    {
        if (deltaDirection.X != 0 || deltaDirection.Y != 0)
        {
            audioPlayer.PlayIfNotPlaying("Movement");
        }
    }
    
    public void SomeoneWasKilled()
    {
        if (AgarioGame.ActivePlayersCount == GameConfig.Data.PlayersVolume)
        {
            audioPlayer.Play("FirstKill");
        }

        if (AgarioGame.ActivePlayersCount == 2)
        {
            audioPlayer.Play("TheLastSurvivor");
        }
            
        audioPlayer.Play("Bite");
    }
}