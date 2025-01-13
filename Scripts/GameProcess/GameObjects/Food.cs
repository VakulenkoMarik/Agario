using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Settings;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Food : GameObject, IDrawable
{
    private Random random = new();
    private CircleShape shape;
    
    public float Kilo { get; private set; } = 0.5f;
    
    public Food(Color fillColor) : base(new CircleShape(3f))
    {
        shape = (CircleShape)ObjectShape;
        shape.FillColor = fillColor;
    }

    public Drawable GetMesh()
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
        shape.Scale = new Vector2f(shape.Radius, shape.Radius);
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);
        shape.Position = Position;
    }
}