// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.Audio;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.Controllers;

public class HumanController : Controller
{
    private Vector2f inputDelta;

    public HumanController(Player newPlayer) : base(newPlayer)
    {
        player = newPlayer;

        Input.RegisterControllerKey(GameConfig.Data.PlayerMoveKeys.KeyToUp, DeltaYToUp, "moveHumanToUp");
        Input.RegisterControllerKey(GameConfig.Data.PlayerMoveKeys.KeyToDown, DeltaYToDown, "moveHumanToDown");
        Input.RegisterControllerKey(GameConfig.Data.PlayerMoveKeys.KeyToLeft, DeltaXToLeft, "moveHumanToLeft");
        Input.RegisterControllerKey(GameConfig.Data.PlayerMoveKeys.KeyToRight, DeltaXToRight, "moveHumanToRight");
        
        Input.RegisterControllerKey(GameConfig.Data.SwapPlayersControllersKey, SwitchBodiesWithRandomPlayer, "swapControllers", false);
    }

    protected override Vector2f GetDirection()
    {
        Vector2f deltaDirection = inputDelta.Normalize();
        inputDelta.Y = 0;
        inputDelta.X = 0;

        AudioSystem.Instance.Play(AudioType.Movement, deltaDirection);
        
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
        int index = Randomizer.Next(0, AgarioGame.controllersList.Count);
        Controller controllerToSwitch = AgarioGame.controllersList[index];

        (controllerToSwitch.player, player) = (player, controllerToSwitch.player);
    }
}