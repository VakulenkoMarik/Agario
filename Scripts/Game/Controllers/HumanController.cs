using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.Controllers;

public class HumanController : Controller
{
    private Vector2f inputDelta;

    public HumanController(Player newPlayer) : base(newPlayer)
    {
        player = newPlayer;

        Input.RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToUp, DeltaYToUp, "moveHumanToUp");
        Input.RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToDown, DeltaYToDown, "moveHumanToDown");
        Input.RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToLeft, DeltaXToLeft, "moveHumanToLeft");
        Input.RegisterControllerKey(Configurations.PlayerMoveKeys.KeyToRight, DeltaXToRight, "moveHumanToRight");
        
        Input.RegisterControllerKey(Configurations.SwapPlayersControllersKey, SwitchBodiesWithRandomPlayer, "swapControllers", false);
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
        int index = Configurations.Randomizer.Next(0, AgarioGame.controllersList.Count);
        Controller controllerToSwitch = AgarioGame.controllersList[index];

        (controllerToSwitch.player, player) = (player, controllerToSwitch.player);
    }
}