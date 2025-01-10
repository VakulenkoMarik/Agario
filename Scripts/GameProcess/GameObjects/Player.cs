using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    public float Size { get; private set; } = 30;
    private float speed = 5f;
    
    private CircleShape shape;

    public Player(Color fillColor) : base()
    {
        ShapeInit(fillColor);
    }

    private void ShapeInit(Color fillColor)
    {
        shape = new CircleShape();
        shape.Radius = Size / 2;
        shape.Position = Position;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        shape.FillColor = fillColor;
    }

    public void Move()
    {
        
    }

    public void Update()
    {
        
    }

    public Shape GetShape()
    {
        return shape;
    }
}