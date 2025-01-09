using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    public float Size { get; private set; }
    
    private float speed = 5f;
    private CircleShape shape;

    public Player(Color fillColor) : base()
    {
        Size = 30;
        
        shape = new CircleShape();
        shape.Radius = Size / 2;
        shape.Position = Position;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        shape.FillColor = fillColor;

        Mesh = shape;
    }

    public void Move()
    {
        
    }

    public void Update()
    {
        
    }

    public void Draw()
    {
        
    }
}