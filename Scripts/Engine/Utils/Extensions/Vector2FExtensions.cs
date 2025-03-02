using SFML.System;

namespace Agario.Scripts.Engine.Utils.Extensions;

public static class Vector2FExtensions
{
    public static Vector2f Normalize(this Vector2f vector)
    {
        float length = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        
        return length > 0 ? vector / length : new Vector2f(0, 0);
    }
}