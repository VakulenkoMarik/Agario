using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.Controllers;
using SFML.Graphics;
using SFML.System;

// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.GameObjects;

public class Player : GameObject, IUpdatable
{
    public float Radius { get; private set; }
    
    private readonly CircleShape shape;

    private Controller movementController = null!;

    public Player(float radius) : base(new CircleShape(radius))
    {
        shape = (CircleShape)ObjectShape;
        
        ShapeInit();
    }

    public void SetController(Controller controller)
    {
        movementController = controller;
    }

    public void Update()
    {
        movementController.Update();
        UpdateMesh();
    }

    private void ShapeInit()
    {
        Color fillColor = shape.FillColor.GenerateColor(10, 255);
        shape.FillColor = fillColor;
        
        shape.Origin = new Vector2f(shape.Radius, shape.Radius);

        Radius = shape.Radius;
    }

    private void UpdateMesh()
    {
        shape.Origin = new Vector2f(Radius, Radius);
        
        shape.Radius = Radius;
        shape.Position = Position;
    }
    
    public void Grow(GameObject target)
    {
        float kiloToAdd = 0;
        
        if (target is Food food)
        {
            kiloToAdd = food.Kilo;
        }
        else if (target is Player player)
        {
            kiloToAdd = player.Radius;
            player.Destroy();
        }
        
        GainWeight(kiloToAdd);
        UpdateMesh();
    }

    private void GainWeight(float kilo)
    {
        Radius += kilo / 2;
        
        movementController.ChangeSpeedByKilo(kilo);
    }

    public void SwapControllersWith(Player playerToSwap)
    {
        playerToSwap.movementController.ChangePlayer(this);
        movementController.ChangePlayer(playerToSwap);
        
        (movementController, playerToSwap.movementController)
            = (playerToSwap.movementController, movementController);
        
    }
}