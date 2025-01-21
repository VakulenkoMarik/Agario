using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.System;
using SFML.Window;

namespace Agario.Scripts.Game.InputProviders;

public class PlayerInputProvider : IInputProvider
{
    public Vector2f GetInput()
    {
        Vector2f inputDelta = new Vector2f();

        if (Keyboard.IsKeyPressed(Configurations.PlayerKeys.KeyToUp)) 
            inputDelta.Y -= 1;
        
        if (Keyboard.IsKeyPressed(Configurations.PlayerKeys.KeyToDown)) 
            inputDelta.Y += 1;
        
        if (Keyboard.IsKeyPressed(Configurations.PlayerKeys.KeyToLeft)) 
            inputDelta.X -= 1;
        
        if (Keyboard.IsKeyPressed(Configurations.PlayerKeys.KeyToRight)) 
            inputDelta.X += 1;

        return inputDelta.Normalize();
    }

    public bool CanSwapBodies()
    {
        if (Keyboard.IsKeyPressed(Keyboard.Key.F))
            return true;

        return false;
    }
}