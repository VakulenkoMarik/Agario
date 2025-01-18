using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Engine.Utils;

public static class FoodListExtensions
{
    public static Food FindNearestFood(this List<Food> foodsList, Vector2f position)
    {
        Food? nearestFood = null;
        float minDistanceSquared = float.MaxValue;

        foreach (var food in foodsList)
        {
            float distanceSquared = CustomMath.DistanceSquared(position, food.Position);

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                nearestFood = food;
            }
        }

        return nearestFood;
    }
}