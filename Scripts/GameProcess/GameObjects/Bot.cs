using Agario.Scripts.Engine.Settings;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Bot : Player
{
    public Bot(Color color) : base(color, Configurations.Random.Next(20, 50)) { }

    private Vector2f targetDirection = new(0, 0);

    public void MakeDecision(List<Food> foodList, List<Player> players)
    {
        targetDirection = new Vector2f(0, 0);
        
        Food? nearestFood = FindNearestFood(foodList);
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
        direction = CustomMath.Normalize(targetDirection);
    }
    
    private Food? FindNearestFood(List<Food> foodList)
    {
        Food? nearestFood = null;
        float minDistance = float.MaxValue;

        foreach (var food in foodList)
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
    
    private Player? FindSmallerPlayer(List<Player> players)
    {
        Player? target = null;
        float minDistance = float.MaxValue;

        foreach (var player in players)
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
    
    private Player? FindBiggerPlayer(List<Player> players)
    {
        Player? threat = null;
        
        float minDistance = float.MaxValue;
        float threatRadius = 100f;

        foreach (var player in players)
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