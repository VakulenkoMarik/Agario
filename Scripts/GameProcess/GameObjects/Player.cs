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
    public float Radius { get; private set; } = 50;
    
    private float speed = 150;
    private Vector2f direction;
    
    private CircleShape shape;

    public Player(Color fillColor) : base()
    {
        ShapeInit(fillColor);
        
        direction = new Vector2f(0, 0);

        float posX = Configurations.WindowWidth / 2;
        float posY = Configurations.WindowHeight / 2;

        Position = new Vector2f(posX, posY);
    }

    private void ShapeInit(Color fillColor)
    {
        shape = new CircleShape(Radius);
        shape.FillColor = fillColor;
        
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
    }

    private bool CanMove(float newX, float newY)
    {
        float xborder = newX + Radius * direction.X;
        float yborder = newY + Radius * direction.Y;
        
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

    private void DirectionProcessing()
    {
        float directionX = 0;
        float directionY = 0;
        
        directionY = (Keyboard.IsKeyPressed(Keyboard.Key.S) ? 1 : 0) - 
                     (Keyboard.IsKeyPressed(Keyboard.Key.W) ? 1 : 0);

        directionX = (Keyboard.IsKeyPressed(Keyboard.Key.D) ? 1 : 0) - 
                     (Keyboard.IsKeyPressed(Keyboard.Key.A) ? 1 : 0);

        direction = new Vector2f(directionX, directionY);
    }

    public void Update()
    {
        DirectionProcessing();
        TryMove();
    }

    public Shape GetShape()
    {
        return shape;
    }
}