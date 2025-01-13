using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine;

public class GameObject
{
    public Vector2f Position;
    public Vector2f Velocity { get; private set; }
    
    public Shape ObjectShape { get; private set; }

    public GameObject(Shape objectShape)
    {
        ObjectShape = objectShape;
        
        Velocity = new (0, 0);
        Position = new (0, 0);

        GameLoop gm = GameLoop.GetInstance();

        if (this is IUpdatable updatable)
        {
            gm.updatableObjects.Add(updatable);
        }

        if (this is IDrawable drawable)
        {
            gm.drawableObjects.Add(drawable);
        }
    }
    
    public void Destroy()
    {
        GameLoop gm = GameLoop.GetInstance();
        
        if (this is IUpdatable updatable)
        {
            gm.updatableObjects.Remove(updatable);
        }

        if (this is IDrawable drawable)
        {
            gm.drawableObjects.Remove(drawable);
        }
    }
}