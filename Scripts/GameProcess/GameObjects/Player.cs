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
    private bool isHuman;
    private float speed = 150;
    
    private Vector2f direction;
    
    private CircleShape shape;

    public Player(Color fillColor, bool isHuman) : base(new CircleShape(35f))
    {
        this.isHuman = isHuman;
        
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
    }

    private void DirectionProcessing()
    {
        float dX = 0f;
        float dY = 0f;
            
        if (isHuman)
        {
            (dX, dY) = HumanDirectionProc();
        }

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
        shape.Position = Position;
    }

    public void Grow(float kilo)
    {
        shape.Radius += kilo / 2;
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
    }

    public Drawable GetMesh()
    {
        return shape;
    }
}