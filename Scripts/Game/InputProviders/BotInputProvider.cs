using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.InputProviders;

public class BotInputProvider(Bot bot) : IInputProvider
{
    public Vector2f GetInput()
    {
        return bot.VectorFromInput();
    }
}