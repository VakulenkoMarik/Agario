// ReSharper disable InconsistentNaming
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8618, CS9264

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Data;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Engine.Utils.Extensions;
using Agario.Scripts.Game.Animations;
using Agario.Scripts.Game.Audio;
using SFML.Graphics;
using SFML.System;
using Time = Agario.Scripts.Engine.Time;

namespace Agario.Scripts.Game.GameObjects;

public class Player : GameObject, IUpdatable, IDrawable
{
    public GameCharacter Character { get; private set; }
    public float Radius { get; private set; }
    private float currentSpeed;
    
    private bool isRun;
    public bool IsRun
    {
        set
        {
            isRun = value;
            currentSpeed = value ? defaultValues.runSpeed : defaultValues.walkSpeed;
            
            if (value)
                audioSystem.Play(AudioType.Running);
        }
    }
    
    private readonly CircleShape shape;
    private readonly AudioSystem audioSystem = ServiceLocator.Instance.Get<AudioSystem>();
    
    private PlayerDefaultValues defaultValues;

    public Player()
    {
        defaultValues = new PlayerDefaultValues();
        currentSpeed = defaultValues.walkSpeed;
        
        shape = (CircleShape)ObjectShape;
        Animator = AnimatorsFactory.CreateAnimator(AnimateObjectType.Player, this);
    }
    
    public void Update()
    {
        UpdateMesh();
    }

    public void ShapeInit(float radius, Color? color = null)
    {
        Color fillColor = color ?? shape.FillColor.GenerateColor(10, 255);

        shape.Radius = radius;
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

            audioSystem.Play(AudioType.SomeoneWasKilled);
        }
        
        GainWeight(kiloToAdd);
        UpdateMesh();
    }

    private void GainWeight(float kilo)
    {
        Radius += kilo / 2;

        ReduceSpeed(ref defaultValues.walkSpeed, kilo);
        ReduceSpeed(ref defaultValues.runSpeed, kilo);
    }
    
    private void ReduceSpeed(ref float speed, float reduction)
    {
        if (speed - reduction >= defaultValues.minSpeed)
            speed -= reduction;
    }

    public CircleShape GetShape()
    {
        return shape;
    }

    public void MakeHappy()
    {
        Animator?.SetTrigger("isHappy");
        audioSystem.Play(AudioType.Happy);
    }
    
    private bool CanMove(float newX, float newY, Vector2f direction)
    {
        float xBorder = newX + Radius * direction.X;
        float yBorder = newY + Radius * direction.Y;
        
        if (xBorder < 0 || xBorder > ProgramConfig.Data.WindowWidth)
            return false;
        
        if (yBorder < 0 || yBorder > ProgramConfig.Data.WindowHeight)
            return false;

        return true;
    }
    
    public void TryMove(Vector2f direction)
    {
        if (direction is not { X: 0, Y: 0 })
        {
            float x = Position.X + currentSpeed * direction.X * Time.deltaTime;
            float y = Position.Y + currentSpeed * direction.Y * Time.deltaTime;

            if (CanMove(x, y, direction))
            {
                Position = new Vector2f(x, y);
        
                Animator?.SetBool("isWalk", true);
                Animator?.SetBool("isRun", isRun);
                
                return;
            }
        }
        
        Animator?.SetBool("isRun", false);
        Animator?.SetBool("isWalk", false);
    }
}

public struct PlayerDefaultValues()
{
    public float walkSpeed = 120;
    public float runSpeed = 220;
    public float minSpeed { get; private set; } = 15;
}