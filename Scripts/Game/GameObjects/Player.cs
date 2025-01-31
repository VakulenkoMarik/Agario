using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using SFML.Graphics;
using SFML.System;
using Time = Agario.Scripts.Engine.Time;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    public float Radius { get; private set; }
    
    private readonly CircleShape shape;
    
    private float speed = 120;
    private const float minSpeed = 15;

    public Player(float radius) : base(new CircleShape(radius))
    {
        shape = (CircleShape)ObjectShape;
        
        ShapeInit();
    }
    
    public void Update()
    {
        UpdateMesh();
    }

    private void ShapeInit()
    {
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
        shape.FillColor = fillColor;
        
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);

        Radius = shape.Radius;
    }

    private void UpdateMesh()
    {
        shape.Origin = new Vector2f(Radius, Radius);
        
        shape.Radius = Radius;
        shape.Position = Position;
    }
    
    public void Grow(GameObject target)
    {
        float kiloToAdd = 0;
        
        if (target is Food food)
        {
            kiloToAdd = food.Kilo;
        }
        else if (target is Player player)
        {
            kiloToAdd = player.Radius;
            player.Destroy();
        }
        
        GainWeight(kiloToAdd);
        UpdateMesh();
    }

    private void GainWeight(float kilo)
    {
        Radius += kilo / 2;

        if (speed - kilo >= minSpeed)
        {
            speed -= kilo;
        }
    }

    public CircleShape GetShape()
    {
        return shape;
    }
    
    private bool CanMove(float newX, float newY, Vector2f direction)
    {
        float xBorder = newX + Radius * direction.X;
        float yBorder = newY + Radius * direction.Y;
        
        if (xBorder < 0 || xBorder > ProgramConfig.Data.WindowWidth)
        {
            return false;
        }
        
        if (yBorder < 0 || yBorder > ProgramConfig.Data.WindowHeight)
        {
            return false;
        }

        return true;
    }
    
    public void TryMove(Vector2f direction)
    {
        float x = Position.X + speed * direction.X * Time.deltaTime;
        float y = Position.Y + speed * direction.Y * Time.deltaTime;

        if (!CanMove(x, y, direction))
        {
            return;
        }
        
        Position = new Vector2f(x, y);
    }
}