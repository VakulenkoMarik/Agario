using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;
using SFML.Graphics;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine;

public class GameObject
{
    public Vector2f Position;
    protected Shape? ObjectShape { get; private set; }

    private GameLoop gameLoop = null!;

    protected GameObject(Shape objectShape)
    {
        ValuesInit();
        
        ObjectShape = objectShape;

        AddObjectToLists();
    }
    
    protected GameObject()
    {
        ValuesInit();

        AddObjectToLists();
    }

    private void ValuesInit()
    {
        Position = new (0, 0);

        gameLoop = GameLoop.GetInstance();
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
    }

    public Drawable GetMesh()
    {
        return ObjectShape;
    }
}