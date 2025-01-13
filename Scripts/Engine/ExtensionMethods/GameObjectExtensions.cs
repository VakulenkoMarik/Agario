using Agario.Scripts.Engine.Settings;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine.ExtensionMethods;

public static class GameObjectExtensions
{
    public static bool CollidesWith(this GameObject gameObject, GameObject target)
    {
        Vector2f object1Center = gameObject.Position;
        Vector2f object2Center = target.Position;

        if (gameObject.ObjectShape is CircleShape c1Shape
            && target.ObjectShape is CircleShape c2Shape)
        {
            float distanceSquared = CustomMath.DistanceSquared(object1Center, object2Center);

            float c1ShapeRSquared = c1Shape.Radius * c1Shape.Radius;
            float c2ShapeRSquared = c2Shape.Radius * c2Shape.Radius;

            return distanceSquared <= c1ShapeRSquared + c2ShapeRSquared;
        }
        
        return false;
    }
}