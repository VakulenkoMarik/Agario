using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.GameObjects;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.Controllers;

public class HumanController : Controller
{
    private Vector2f inputDelta;
    
    private readonly Player controlPlayer;

    public HumanController(Player player) : base(player)
    {
        controlPlayer = player;

        RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToUp, DeltaYToUp, "moveHumanToUp");
        RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToDown, DeltaYToDown, "moveHumanToDown");
        RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToLeft, DeltaXToLeft, "moveHumanToLeft");
        RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToRight, DeltaXToRight, "moveHumanToRight");
        
        RegisterControllerKey(Configurations.SwapPlayersControllersKey, SwitchBodiesWithRandomPlayer, "swapControllers", false);
    }

    protected override Vector2f GetDirection()
    {
        Vector2f deltaDirection = inputDelta.Normalize();
        inputDelta.Y = 0;
        inputDelta.X = 0;
        
        return deltaDirection;
    }

    private void DeltaYToUp()
        => inputDelta.Y -= 1;

    private void DeltaYToDown()
        => inputDelta.Y += 1;
    
    private void DeltaXToLeft()
        => inputDelta.X -= 1;
    
    private void DeltaXToRight()
        => inputDelta.X += 1;
    
    private void SwitchBodiesWithRandomPlayer()
    {
        int index = Configurations.Randomizer.Next(0, AgarioGame.playersList.Count);
        Player playerToSwitch = AgarioGame.playersList[index];

        controlPlayer.SwapControllersWith(playerToSwitch);
    }
}