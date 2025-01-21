using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public static class Time
{
    private const int TargetFPS = 60;
    private const float UntilUpdateTime = 1f / TargetFPS;
    
    public static float deltaTime { get; private set; }
    private static readonly Clock clock = new();

    public static void Update()
    {
        clock.Restart();
        
        while (clock.ElapsedTime.AsSeconds() < UntilUpdateTime) {}

        deltaTime = clock.ElapsedTime.AsSeconds();
    }
}