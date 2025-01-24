using Agario.Scripts.Engine.InputSystem;
using Agario.Scripts.Engine.Utils;
using Agario.Scripts.Game.GameObjects;
using SFML.System;
using SFML.Window;
using Time = Agario.Scripts.Engine.Time;
// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Game.Controllers;

public class Controller
{
    private Player player;
    private Vector2f direction = new(0, 0);
        
    private float speed = 120;
    private const float minSpeed = 15;

    protected Controller(Player player)
    {
        this.player = player;
    }

    public void ChangePlayer(Player newPlayer)
    {
        player = newPlayer;
    }
    
    public void Update()
    {
        direction = GetDirection();
        TryMove();
    }

    protected virtual Vector2f GetDirection()
    {
        return direction;
    }

    private bool CanMove(float newX, float newY)
    {
        float xBorder = newX + player.Radius * direction.X;
        float yBorder = newY + player.Radius * direction.Y;
        
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
        float x = player.Position.X + speed * direction.X * Time.deltaTime;
        float y = player.Position.Y + speed * direction.Y * Time.deltaTime;

        if (!CanMove(x, y))
        {
            return;
        }
        
        player.Position = new Vector2f(x, y);
    }

    public void ChangeSpeedByKilo(float kilo)
    {
        if (speed > minSpeed)
        {
            speed -= kilo;
        }
    }

    protected void RegisterControllerKey(Keyboard.Key eventKey, Action action, string name, bool onKeyPressed = true)
    {
        Input.AddKey(eventKey, name);

        if (onKeyPressed)
        {
            Input.AddEventOnPressedToKey(action, name);
            return;
        }
        
        Input.AddEventOnDownToKey(action, name);
    }
}