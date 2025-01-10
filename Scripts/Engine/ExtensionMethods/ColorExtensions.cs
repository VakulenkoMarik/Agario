using SFML.Graphics;

namespace Agario.Scripts.Engine.ExtensionMethods;

public static class ColorExtensions
{
    private static Random random = new();
    
    public static Color GenerateColor(this Color color, byte minValue, byte maxValue)
    {
        byte channelMaxValue = 255;
        byte channelMinValue = 10;
        
        byte r = (byte)random.Next(channelMinValue, channelMaxValue);
        byte g = (byte)random.Next(channelMinValue, channelMaxValue);
        byte b = (byte)random.Next(channelMinValue, channelMaxValue);

        color = new Color(r, g, b);

        return color;
    }
}