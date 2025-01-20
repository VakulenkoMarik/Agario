using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine.Utils;

public static class GameObjectExtensions
{
    public static bool CollidesWith(this GameObject gameObject, GameObject target)
    {
        Vector2f object1Center = gameObject.Position;
        Vector2f object2Center = target.Position;

        if (gameObject.GetMesh() is CircleShape c1Shape
            && target.GetMesh() is CircleShape c2Shape)
        {
            float distanceSquared = CustomMath.DistanceSquared(object1Center, object2Center);
            
            float sumOfRadiuses = c1Shape.Radius + c2Shape.Radius;
            
            return distanceSquared <= sumOfRadiuses * sumOfRadiuses;
        }
        
        return false;
    }
    
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