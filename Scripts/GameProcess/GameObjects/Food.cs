using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Food : GameObject, IUpdatable, IDrawable
{
    private Random random = new();
    private CircleShape shape;

    public float Radius { get; private set; } = 3f;
    
    public Food(Color fillColor) : base()
    {
        shape = new CircleShape(Radius);
        shape.FillColor = fillColor;
    }

    public void Update()
    {
        
    }

    public Shape GetShape()
    {
        return shape;
    }

    public void PutOnMap()
    {
        int x = random.Next(0, Configurations.WindowWidth);
        int y = random.Next(0, Configurations.WindowHeight);
        
        Position = new Vector2f(x, y);
        
        UpdateMesh();
    }
    
    private void UpdateMesh()
    {
        shape.Scale = new Vector2f(Radius, Radius);
        shape.Origin = new Vector2f(Radius, Radius);
        shape.Position = Position;
    }
}