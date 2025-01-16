using Agario.Scripts.Engine.Utils;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.GameProcess.GameObjects;

public class Bot(List<Food> foods, List<Player> players) : Player(Configurations.Randomizer.Next(20, 50))
{
    private Vector2f targetDirection;

    private void MakeDecision()
    {
        targetDirection = new Vector2f(0, 0);
        
        Food? nearestFood = FindNearestFood(foods);
        if (nearestFood != null)
        {
            targetDirection += CustomMath.Normalize(nearestFood.Position - Position) * 0.5f;
        }
        
        Player? smallerPlayer = FindSmallerPlayer(players);
        if (smallerPlayer != null)
        {
            targetDirection += CustomMath.Normalize(smallerPlayer.Position - Position) * 1.0f;
        }
        
        Player? biggerPlayer = FindBiggerPlayer(players);
        if (biggerPlayer != null)
        {
            targetDirection -= CustomMath.Normalize(biggerPlayer.Position - Position) * 2.0f;
        }
    }

    public override void DirectionProcessing()
    {
        MakeDecision();
        direction = CustomMath.Normalize(targetDirection);
    }
    
    private Food? FindNearestFood(List<Food> foodsList)
    {
        Food? nearestFood = null;
        float minDistance = float.MaxValue;

        foreach (var food in foodsList)
        {
            float distance = CustomMath.DistanceSquared(Position, food.Position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestFood = food;
            }
        }

        return nearestFood;
    }
    
    private Player? FindSmallerPlayer(List<Player> playersList)
    {
        Player? target = null;
        float minDistance = float.MaxValue;

        foreach (var player in playersList)
        {
            if (player == this || player.Radius >= Radius)
                continue;

            float distance = CustomMath.DistanceSquared(Position, player.Position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                target = player;
            }
        }

        return target;
    }
    
    private Player? FindBiggerPlayer(List<Player> playersList)
    {
        Player? threat = null;
        
        float minDistance = float.MaxValue;
        float threatRadius = 100f;

        foreach (var player in playersList)
        {
            if (player == this || player.Radius <= Radius)
                continue;

            float distance = CustomMath.DistanceSquared(Position, player.Position);

            if (distance < minDistance && distance <= threatRadius * threatRadius)
            {
                minDistance = distance;
                threat = player;
            }
        }

        return threat;
    }
}