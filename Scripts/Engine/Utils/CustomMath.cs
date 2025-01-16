using SFML.System;

namespace Agario.Scripts.Engine.Utils;

public static class CustomMath
{
    public static float DistanceSquared(Vector2f vectorA, Vector2f vectorB)
    {
        float aToBProjectionX = (vectorB.X - vectorA.X) * (vectorB.X - vectorA.X);
        float aToBProjectionY = (vectorB.Y - vectorA.Y) * (vectorB.Y - vectorA.Y);

        float distance = aToBProjectionX + aToBProjectionY;
        
        return distance;
    }
    
    public static Vector2f Normalize(Vector2f vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        
        return length > 0 ? vector / length : new Vector2f(0, 0);
    }
}