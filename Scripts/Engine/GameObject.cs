using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine;

public class GameObject
{
    public Vector2f Position;
    public Vector2f Velocity { get; private set; }

    public GameObject()
    {
        Velocity = new (0, 0);
        Position = new Vector2f(0, 0);

        if (this is IUpdatable updatable)
        {
            GameLoop.updatableObjects.Add(updatable);
        }

        if (this is IDrawable drawable)
        {
            GameLoop.drawableObjects.Add(drawable);
        }
    }

    public void Destroy()
    {
        if (this is IUpdatable updatable)
        {
            GameLoop.updatableObjects.Remove(updatable);
        }

        if (this is IDrawable drawable)
        {
            GameLoop.drawableObjects.Remove(drawable);
        }
    }
}