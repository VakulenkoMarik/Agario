using SFML.System;

namespace Agario.Scripts.Engine.Utils;

public static class ListExtensions
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

    public static void RemoveSwap<T>(this List<T> list, T objectToRemove)
    {
        int removingIndex = list.IndexOf(objectToRemove);

        if (removingIndex >= 0)
        {
            if (removingIndex < list.Count - 1)
            {
                list[removingIndex] = list[^1];
            }
            list.RemoveAt(list.Count - 1);
        }
    }
}