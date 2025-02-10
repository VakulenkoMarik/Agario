#pragma warning disable CS8604 // Possible null reference argument.

using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.Controllers;
using Agario.Scripts.Game.GameObjects;

namespace Agario.Scripts.Game.CustomExtensions;

public static class PlayerExtension
{
    public static void FoodLunchAttempt(this Player eater, Food food)
    {
        if (eater.CollidesWith(food))
        {
            eater.Grow(food);
            AgarioGame.AddToDeathList(food);
        }
    }
    
    public static void PlayerLunchAttempt(this Player eater, Controller victim)
    {
        if (eater.CollidesWith(victim.player))
        {
            eater.Grow(victim.player);
            AgarioGame.AddToDeathList(victim);
        }
    }
}