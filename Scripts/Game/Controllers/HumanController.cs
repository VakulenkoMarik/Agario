using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
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

        Input.RegisterControllerKey(GameData.PlayerMoveKeys.KeyToUp, DeltaYToUp, "moveHumanToUp");
        Input.RegisterControllerKey(GameData.PlayerMoveKeys.KeyToDown, DeltaYToDown, "moveHumanToDown");
        Input.RegisterControllerKey(GameData.PlayerMoveKeys.KeyToLeft, DeltaXToLeft, "moveHumanToLeft");
        Input.RegisterControllerKey(GameData.PlayerMoveKeys.KeyToRight, DeltaXToRight, "moveHumanToRight");
        
        Input.RegisterControllerKey(GameData.SwapPlayersControllersKey, SwitchBodiesWithRandomPlayer, "swapControllers", false);
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