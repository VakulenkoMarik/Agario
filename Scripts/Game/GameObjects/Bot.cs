using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.System;

// ReSharper disable InconsistentNaming
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace Agario.Scripts.Game.GameObjects;

public class Bot() : Player(Configurations.Randomizer.Next(10, 30))
{
    private Vector2f targetDirection;

    private readonly float distanceOfView = 4000f;

    public Vector2f VectorFromInput()
    {
        MakeDecision();
        return targetDirection.Normalize();
    }
    
    private void MakeDecision()
    {
        targetDirection = new Vector2f(0, 0);

        Player nearestBiggerPlayer = FindClosestPlayer(AgarioGame.playersList, IsBiggerPlayer);
        AddDirectionAwayFrom(nearestBiggerPlayer, 4.0f);
        
        Player nearestSmallerPlayer = FindClosestPlayer(AgarioGame.playersList, IsSmallerPlayer);
        AddDirectionTowards(nearestSmallerPlayer, 2.0f);
        
        Food nearestFood = AgarioGame.foodList.FindClosestGameObject(Position);
        AddDirectionTowards(nearestFood, 0.5f);
    }
    
    private void AddDirection(GameObject? target, float weight, bool isTowards)
    {
        if (target != null)
        {
            float distanceSquared = CustomMath.DistanceSquared(Position, target.Position);

            if (distanceSquared <= distanceOfView * distanceOfView)
            {
                float scaledWeight = weight / (distanceSquared + 1);
        
                Vector2f newDirection = target.Position - Position;
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

    private Player FindClosestPlayer(List<Player> playersList, Func<Player, bool> condition)
    {
        Player closestPlayer = null;
        
        float minDistanceSquared = float.MaxValue;
        float distanceOfViewSquared = distanceOfView * distanceOfView;

        foreach (var player in playersList)
        {
            if (player == this || !condition(player))
            {
                continue;
            }

            float distanceSquared = CustomMath.DistanceSquared(Position, player.Position);

            if (distanceSquared < minDistanceSquared && distanceSquared <= distanceOfViewSquared)
            {
                minDistanceSquared = distanceSquared;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
    
    private bool IsSmallerPlayer(Player player) => player.Radius < Radius;
    private bool IsBiggerPlayer(Player player) => player.Radius > Radius;
    
}
