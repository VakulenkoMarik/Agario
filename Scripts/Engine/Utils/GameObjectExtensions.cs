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
}