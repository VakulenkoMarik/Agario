using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.InputProviders;
using SFML.Graphics;
using SFML.System;
using Time = Agario.Scripts.Engine.Time;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.GameObjects;

public class Player : GameObject, IUpdatable, IInputHandler
{
    public float Radius { get; private set; }
    
    private float speed = 120;
    private readonly float minSpeed = 15;

    private readonly CircleShape shape;
    private IInputProvider InputProvider;
    
    private Vector2f direction;

    public Player(float radius) : base(new CircleShape(radius))
    {
        InputProvider = ChoseInputProvider();
        
        shape = (CircleShape)ObjectShape;
        
        direction = new Vector2f(0, 0);
        
        ShapeInit();
        PositionInit();
    }

    private IInputProvider ChoseInputProvider()
    {
        if (this is Bot bot)
        {
            return new BotInputProvider(bot);
        }
        
        return new PlayerInputProvider();
    }

    public void Drop(float x, float y)
    {
        Position = new Vector2f(x, y);
    }

    private void ShapeInit()
    {
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
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
        TryMove();
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
        
        UpdatePosition(new Vector2f(x, y));
    }

    private void UpdatePosition(Vector2f vector)
    {
        Position = vector;
        
        shape.Position = Position;
    }

    private void UpdateMesh()
    {
        shape.Radius = Radius;
        shape.Origin = new Vector2f(Radius, Radius);
    }

    public void Grow(Food food)
    {
        GainWeight(food.Kilo);
        
        UpdateMesh();
    }
    
    public void Grow(Player playerFood)
    {
        GainWeight(playerFood.Radius);
        playerFood.Destroy();
        
        UpdateMesh();
    }

    private void GainWeight(float kilo)
    {
        Radius += kilo / 2;

        if (speed > minSpeed)
        {
            speed -= kilo / 1.5f;
        }
    }

    public void HandleInput()
    {
        direction = InputFromProvider();

        if (InputProvider is PlayerInputProvider provider)
        {
            if (provider.CanSwapBodies())
            {
                SwitchBodiesWith();
            }
        }
    }

    private Vector2f InputFromProvider()
        => InputProvider.GetInput();

    public void ChangeInputProvider(IInputProvider provider)
    {
        InputProvider = provider;
    }

    private void SwitchBodiesWith()
    {
        int index = Configurations.Randomizer.Next(0, AgarioGame.playersList.Count);
        Player playerToSwitch = AgarioGame.playersList[index];

        if (playerToSwitch == this)
        {
            return;
        }

        ChangeInputProvider(new BotInputProvider(playerToSwitch as Bot));

        playerToSwitch.ChangeInputProvider(new PlayerInputProvider());
    }
}