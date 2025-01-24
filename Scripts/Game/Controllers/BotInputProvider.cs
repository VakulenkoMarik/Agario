using Agario.Scripts.Game.GameObjects;
using SFML.System;

namespace Agario.Scripts.Game.Controllers;

public class BotInputProvider(Bot bot)
{
    public Vector2f GetInput()
    {
        return bot.VectorFromInput();
    }
}