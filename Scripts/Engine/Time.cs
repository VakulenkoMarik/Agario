using System.Diagnostics;
using SFML.System;

namespace Agario.Scripts.Engine;

public static class Time
{
    private const int TargetFPS = 60;
    public const float UntilUpdateTime = 1f / TargetFPS;
    
    public static float deltaTime { get; private set; }
    private static Clock clock = new();

    public static void Update()
    {
        clock.Restart();
        
        while (clock.ElapsedTime.AsSeconds() < UntilUpdateTime) {}

        deltaTime = clock.ElapsedTime.AsSeconds();
    }
}