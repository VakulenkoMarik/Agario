using SFML.System;

namespace Agario.Scripts.Engine.Interfaces;

public interface IInputProvider
{
    Vector2f GetInput();
}