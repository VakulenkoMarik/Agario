using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Utils;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.GameProcess.GameObjects;

public class Bot(List<Food> foods, List<Player> players) : Player(Configurations.Randomizer.Next(10, 30))
{
    private Vector2f targetDirection;

    private readonly float fieldOfView = 5000f;

    private void MakeDecision()
    {
        targetDirection = new Vector2f(0, 0);

        Player? nearestBiggerPlayer = FindPlayer(players, IsBiggerPlayer);
        AddDirectionAwayFrom(nearestBiggerPlayer, 2.0f);
        
        Player? nearestSmallerPlayer = FindPlayer(players, IsSmallerPlayer);
        AddDirectionTowards(nearestSmallerPlayer, 1.0f);
        
        Food? nearestFood = FindNearestFood(foods);
        AddDirectionTowards(nearestFood, 0.5f);
    }

    public override void DirectionProcessing()
    {
        MakeDecision();
        direction = CustomMath.Normalize(targetDirection);
    }

    private void AddDirection(GameObject? target, float weight, bool isTowards)
    {
        if (target == null)
        {
            return;
        }

        float distanceSquared = CustomMath.DistanceSquared(Position, target.Position);

        if (distanceSquared > fieldOfView * fieldOfView)
        {
            return;
        }
        
        float scaledWeight = weight / (distanceSquared + 1);
        Vector2f newDirection = CustomMath.Normalize(target.Position - Position) * scaledWeight;
        
        targetDirection += isTowards ? newDirection : -newDirection;
    }
    
    private void AddDirectionTowards(GameObject? target, float weight)
    {
        AddDirection(target, weight, true);
    }

    private void AddDirectionAwayFrom(GameObject? target, float weight)
    {
        AddDirection(target, weight, false);
    }

    private Food? FindNearestFood(List<Food> foodsList)
    {
        Food? nearestFood = null;
        float minDistanceSquared = float.MaxValue;

        foreach (var food in foodsList)
        {
            float distanceSquared = CustomMath.DistanceSquared(Position, food.Position);

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                nearestFood = food;
            }
        }

        return nearestFood;
    }

    private Player? FindPlayer(List<Player> playersList, Func<Player, bool> condition)
    {
        Player? closestPlayer = null;
        float minDistanceSquared = float.MaxValue;
        float fieldOfViewSquared = fieldOfView * fieldOfView;

        foreach (var player in playersList)
        {
            if (player == this || !condition(player))
                continue;

            float distanceSquared = CustomMath.DistanceSquared(Position, player.Position);

            if (distanceSquared < minDistanceSquared && distanceSquared <= fieldOfViewSquared)
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
