namespace Agario.Scripts.Engine.Utils;

public static class Randomizer
{
    private static readonly Random Rand = new();

    public static int Next(int minValue, int maxValue)
    {
        return Rand.Next(minValue, maxValue);
    }
}