using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class GameObject : IDrawable
{
    public Vector2f Position;
    protected Shape ObjectShape { get; private set; }

    private readonly GameLoop gameLoop;

    protected GameObject(Shape objectShape)
    {
        ObjectShape = objectShape;
        Position = new (0, 0);

        gameLoop = GameLoop.GetInstance();

        AddObjectToLists();
    }

    private void AddObjectToLists()
    {
        if (this is IUpdatable updatable)
        {
            gameLoop.updatableObjects.Add(updatable);
        }

        if (this is IDrawable drawable)
        {
            gameLoop.drawableObjects.Add(drawable);
        }

        if (this is IInputHandler handler)
        {
            gameLoop.inputHandlerObjects.Add(handler);
        }
    }

    protected void Destroy()
    {
        if (this is IUpdatable updatable)
        {
            gameLoop.updatableObjects.RemoveSwap(updatable);
        }

        if (this is IDrawable drawable)
        {
            gameLoop.drawableObjects.RemoveSwap(drawable);
        }
        
        if (this is IInputHandler handler)
        {
            gameLoop.inputHandlerObjects.RemoveSwap(handler);
        }
    }

    public Drawable GetMesh()
    {
        return ObjectShape;
    }
}