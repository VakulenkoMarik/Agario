// ReSharper disable InconsistentNaming

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.Game.GameObjects;

public class Food : GameObject, IDrawable
{
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

    public void PutOnMap()
    {
        int x = Randomizer.Next(0, ProgramConfig.Data.WindowWidth);
        int y = Randomizer.Next(0, ProgramConfig.Data.WindowHeight);
        
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