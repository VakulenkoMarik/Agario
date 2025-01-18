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
}