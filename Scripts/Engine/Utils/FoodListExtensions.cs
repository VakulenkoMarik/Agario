using SFML.System;

namespace Agario.Scripts.Engine.Utils;

public static class FoodListExtensions
{
    public static T FindClosestGameObject<T>(this List<T> list, Vector2f position) where T : GameObject
    {
        GameObject? nearestGameObject = null;
        float minDistanceSquared = float.MaxValue;

        foreach (var go in list)
        {
            float distanceSquared = CustomMath.DistanceSquared(position, go.Position);

            if (distanceSquared < minDistanceSquared)
            {
                minDistanceSquared = distanceSquared;
                nearestGameObject = go;
            }
        }

        return nearestGameObject as T;
    }
}