using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;
using SFML.System;

namespace Agario.Scripts.GameProcess.GameObjects;

public class Food : GameObject, IUpdatable, IDrawable
{
    public float Radius { get; private set; }
    
    private Sprite shape;
    
    public Food() : base()
    {
        Radius = 3;
        shape = new Sprite();
        
        UpdateMesh(0.5f);
    }

    public void Update()
    {
        
    }

    public void Draw()
    {
        
    }
    
    private void UpdateMesh(float scale)
    {
        shape.Origin = new Vector2f(Radius, Radius);
        shape.Position = Position;

        Mesh = shape;
        Mesh.Scale = new Vector2f(scale, scale);
    }
}