// ReSharper disable InconsistentNaming

using SFML.Window;

namespace Agario.Scripts.Engine.InputSystem;

public record EventKey(Keyboard.Key eventKey)
{
    private Action? onKeyDownAction;
    private Action? onKeyPressedAction;
    private Action? onKeyUpAction;

    private bool isKeyPressed;
    private bool wasKeyPressed;

    public void SetOnKeyDownEvent(Action action)
        => onKeyDownAction += action;

    public void SetOnKeyPressedEvent(Action action)
        => onKeyPressedAction += action;

    public void SetOnKeyUpEvent(Action action)
        => onKeyUpAction += action;

    public void UpdateStatus()
    {
        wasKeyPressed = isKeyPressed;
        isKeyPressed = Keyboard.IsKeyPressed(eventKey);
    }

    public void TryActivateEvent()
    {
        if (!wasKeyPressed && isKeyPressed)
        {
            onKeyDownAction?.Invoke();
        }
        else if (wasKeyPressed && isKeyPressed)
        {
            onKeyPressedAction?.Invoke();
        }
        else if (wasKeyPressed && !isKeyPressed)
        {
            onKeyUpAction?.Invoke();
        }
    }
}