// ReSharper disable InconsistentNaming
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.Controllers;

public class BotController(Player player) : Controller(player)
{
    private Vector2f targetDirection;

    private readonly float distanceOfView = 6000f;
    
    public void Delete()
    {
        player = null;
        Destroy();
    }

    protected override Vector2f GetDirection()
    {
        MakeDecision();
        return targetDirection.Normalize();
    }
    
    private void MakeDecision()
    {
        targetDirection = new Vector2f(0, 0);

        Player nearestBiggerPlayer = FindClosestPlayer(AgarioGame.controllersList, IsBiggerPlayer);
        AddDirectionAwayFrom(nearestBiggerPlayer, 4.0f);
        
        Player nearestSmallerPlayer = FindClosestPlayer(AgarioGame.controllersList, IsSmallerPlayer);
        AddDirectionTowards(nearestSmallerPlayer, 2.0f);

        if (player != null)
        {
            Food nearestFood = AgarioGame.foodList.FindClosestGameObject(player.Position);
            AddDirectionTowards(nearestFood, 0.5f);
        }
    }
    
    private void AddDirection(GameObject? target, float weight, bool isTowards)
    {
        if (target != null)
        {
            float distanceSquared = CustomMath.DistanceSquared(player.Position, target.Position);

            if (distanceSquared <= distanceOfView * distanceOfView)
            {
                float scaledWeight = weight / (distanceSquared + 1);
        
                Vector2f newDirection = target.Position - player.Position;
                newDirection = newDirection.Normalize() * scaledWeight;
        
                targetDirection += isTowards ? newDirection : -newDirection;
            }
        }
    }
    
    private void AddDirectionTowards(GameObject? target, float weight)
    {
        AddDirection(target, weight, true);
    }

    private void AddDirectionAwayFrom(GameObject? target, float weight)
    {
        AddDirection(target, weight, false);
    }

    private Player FindClosestPlayer(List<Controller> controllersList, Func<Player, bool> condition)
    {
        Player closestPlayer = null;
        
        float minDistanceSquared = float.MaxValue;
        float distanceOfViewSquared = distanceOfView * distanceOfView;

        foreach (var controllerInList in controllersList)
        {
            Player target = controllerInList.player;
            
            if (target is null || target == player || !condition(target))
            {
                continue;
            }

            float distanceSquared = CustomMath.DistanceSquared(player.Position, target.Position);

            if (distanceSquared < minDistanceSquared && distanceSquared <= distanceOfViewSquared)
            {
                minDistanceSquared = distanceSquared;
                closestPlayer = target;
            }
        }

        return closestPlayer;
    }
    
    private bool IsSmallerPlayer(Player target) => target.Radius < player.Radius;
    private bool IsBiggerPlayer(Player target) => target.Radius > player.Radius;
}