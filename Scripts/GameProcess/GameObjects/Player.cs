using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Time = Agario.Scripts.Engine.Time;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.GameProcess.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    private float speed = 120;
    private readonly float minSpeed = 15;

    private readonly CircleShape shape;
    
    public float Radius { get; private set; }
    
    protected Vector2f direction;

    public Player(float radius) : base(new CircleShape(radius))
    {
        Radius = radius;
        
        shape = (CircleShape)ObjectShape;
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
        
        direction = new Vector2f(0, 0);
        
        ShapeInit(fillColor);
        PositionInit();
    }

    public void Drop(float x, float y)
    {
        Position = new Vector2f(x, y);
    }

    private void ShapeInit(Color fillColor)
    {
        shape.FillColor = fillColor;
        
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);

        Radius = shape.Radius;
    }
    
    private void PositionInit()
    {
        float posX = Configurations.WindowWidth / 2f;
        float posY = Configurations.WindowHeight / 2f;

        Position = new Vector2f(posX, posY);
    }
    
    public void Update()
    {
        DirectionProcessing();
        TryMove();
        UpdateMesh();
    }

    public virtual void DirectionProcessing()
    {
        (float dX, float dY) = HumanDirectionProc();

        direction = new Vector2f(dX, dY);
    }

    private (float, float) HumanDirectionProc()
    {
        float directionX = (Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1 : 0) - 
                          (Keyboard.IsKeyPressed(Keyboard.Key.A) ? 1 : 0);
        
        float directionY = (Keyboard.IsKeyPressed(Keyboard.Key.S) ? 1 : 0) - 
                          (Keyboard.IsKeyPressed(Keyboard.Key.W) ? 1 : 0);
        
        return (directionX, directionY);
    }

    private bool CanMove(float newX, float newY)
    {
        float xBorder = newX + shape.Radius * direction.X;
        float yBorder = newY + shape.Radius * direction.Y;
        
        if (xBorder is < 0 or > Configurations.WindowWidth)
        {
            return false;
        }
        
        if (yBorder is < 0 or > Configurations.WindowHeight)
        {
            return false;
        }

        return true;
    }

    private void TryMove()
    {
        float x = Position.X + speed * direction.X * Time.deltaTime;
        float y = Position.Y + speed * direction.Y * Time.deltaTime;

        if (!CanMove(x, y))
        {
            return;
        }

        Position = new Vector2f(x, y);
    }

    private void UpdateMesh()
    {
        shape.Position = Position;
        shape.Radius = Radius;
        shape.Origin = new Vector2f(Radius, Radius);
    }

    public void Grow(Food food)
    {
        GainWeight(food.Kilo);
    }
    
    public void Grow(Player playerFood)
    {
        GainWeight(playerFood.Radius);
        playerFood.Destroy();
    }

    private void GainWeight(float kilo)
    {
        Radius += kilo / 2;

        if (speed > minSpeed)
        {
            speed -= kilo / 1.5f;
        }
    }

    public Drawable GetMesh()
    {
        return shape;
    }
}