// ReSharper disable InconsistentNaming
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.Controllers;

public class Controller : GameObject, IUpdatable
{
    public Player? player { get; protected internal set; }
    private Vector2f direction = new(0, 0);
    
    private PauseActivator pauseActivator => ServiceLocator.Instance.Get<PauseActivator>();

    protected Controller(Player player) : base(player.GetShape())
    {
        this.player = player;
        
    }

    public void ChangePlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    
    public void Update()
    {
        if (player is not null && !pauseActivator.IsPause)
        {
            direction = GetDirection();
            player.TryMove(direction);
        }
        
    }

    protected virtual Vector2f GetDirection()
    {
        return direction;
    }
}