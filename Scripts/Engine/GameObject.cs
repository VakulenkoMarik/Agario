using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine;

public class GameObject
{
    private Vector2f position = new Vector2f(0, 0);
    public Vector2f Position
    {
        get { return position; }
        set { position = value; Mesh.Position = position; }
    }

    public Vector2f Velocity { get; private set; }
    public Transformable Mesh;

    public GameObject()
    {
        Velocity = new (0, 0);

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