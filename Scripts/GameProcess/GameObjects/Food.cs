using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;
using SFML.System;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.GameProcess.GameObjects;

public class Food : GameObject, IDrawable
{
    private readonly Random random = Configurations.Randomizer;
    private readonly CircleShape shape;
    
    public float Kilo { get; private set; } = 0.5f;
    
    public Food() : base(new CircleShape(2f))
    {
        shape = (CircleShape)ObjectShape;
        
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
        shape.FillColor = fillColor;
    }

    public void NewFood()
    {
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
        shape.FillColor = fillColor;
        
        PutOnMap();
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