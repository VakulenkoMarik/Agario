using SFML.Graphics;

// ReSharper disable InconsistentNaming
// ReSharper disable RedundantAssignment

namespace Agario.Scripts.Engine.Utils;

public static class ColorExtensions
{
    private static readonly Random random = new();
    
    public static Color GenerateColor(this Color color, byte minValue, byte maxValue)
    {
        byte r = (byte)random.Next(minValue, maxValue);
        byte g = (byte)random.Next(minValue, maxValue);
        byte b = (byte)random.Next(minValue, maxValue);

        color = new Color(r, g, b);

        return color;
    }
}