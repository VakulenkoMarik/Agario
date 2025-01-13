using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Time = Agario.Scripts.Engine.Time;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    private float speed = 150;

    public float Radius { get; private set; }
    
    protected Vector2f direction;
    
    private CircleShape shape;

    public Player(Color fillColor, float radius) : base(new CircleShape(radius))
    {
        Radius = radius;
        shape = (CircleShape)ObjectShape;
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
    
    protected bool CanMove(float newX, float newY)
    {
        float xborder = newX + shape.Radius * direction.X;
        float yborder = newY + shape.Radius * direction.Y;
        
        if (xborder is < 0 or > Configurations.WindowWidth)
        {
            return false;
        }
        
        if (yborder is < 0 or > Configurations.WindowHeight)
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
        Radius += food.Kilo / 2;
        food.Destroy();
    }
    
    public void Grow(Player playerFood)
    {
        Radius += playerFood.Radius / 2;
        playerFood.Destroy();
    }

    public Drawable GetMesh()
    {
        return shape;
    }
}