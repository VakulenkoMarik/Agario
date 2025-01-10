using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine.ExtensionMethods;

public static class GameObjectExtensions
{
    public static bool CollidesWith(this GameObject gameObject, GameObject target)
    {
        Vector2f object1Center = gameObject.Position;
        Vector2f object2Center = target.Position;

        if (gameObject.ObjectShape is CircleShape && target.ObjectShape is CircleShape)
        {
            CircleShape c1Shape = (CircleShape)gameObject.ObjectShape;
            CircleShape c2Shape = (CircleShape)target.ObjectShape;
            
            float distance = (float)Math.Sqrt(Math.Pow(object2Center.X - object1Center.X, 2) + Math.Pow(object2Center.Y - object1Center.Y, 2));

            return distance <= c1Shape.Radius + c2Shape.Radius;
        }
        
        return false;
    }
}