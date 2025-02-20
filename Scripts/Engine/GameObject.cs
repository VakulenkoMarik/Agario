// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Engine;

public class GameObject
{
    public Vector2f Position;
    protected Shape? ObjectShape { get; private set; }
    public Animator? Animator { get; init; }

    protected GameObject(Shape? objectShape = null)
    {
        ValuesInit();

        if (objectShape is not null)
        {
            ObjectShape = objectShape;
        }

        AddObjectToLists();
    }

    private void ValuesInit()
        => Position = new (0, 0);

    private void AddObjectToLists()
    {
        if (this is IUpdatable updatable)
            SceneLoader.GetCurrentScene().AddUpdatableObject(updatable);
        
        if (this is IDrawable drawable)
            SceneLoader.GetCurrentScene().AddDrawableObject(drawable);
    }

    protected void Destroy()
    {
        if (this is IUpdatable updatable)
            SceneLoader.GetCurrentScene().DestroyUpdatableObject(updatable);
        
        if (this is IDrawable drawable)
            SceneLoader.GetCurrentScene().DestroyDrawableObject(drawable);
    }

    public Drawable GetMesh()
        => ObjectShape;
}