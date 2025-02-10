// ReSharper disable InconsistentNaming
// ReSharper disable RedundantAssignment

using SFML.Graphics;

namespace Agario.Scripts.Engine.Utils.Extensions;

public static class ColorExtensions
{
    public static Color GenerateColor(this Color color, byte minValue, byte maxValue)
    {
        byte r = (byte)Randomizer.Next(minValue, maxValue);
        byte g = (byte)Randomizer.Next(minValue, maxValue);
        byte b = (byte)Randomizer.Next(minValue, maxValue);

        color = new Color(r, g, b);

        return color;
    }
}