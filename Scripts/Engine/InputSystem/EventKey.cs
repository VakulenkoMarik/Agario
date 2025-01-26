// ReSharper disable InconsistentNaming

using SFML.Window;

namespace Agario.Scripts.Engine.InputSystem;

public record EventKey(Keyboard.Key eventKey)
{
    private Action? onKeyDownAction;
    private Action? onKeyPressedAction;

    private bool isKeyPressed;
    private bool wasKeyPressed;

    public void SetOnKeyDownEvent(Action action)
    {
        onKeyDownAction += action;
    }
    
    public void SetOnKeyPressedEvent(Action action)
    {
        onKeyPressedAction += action;
    }

    public void UpdateStatus()
    {
        wasKeyPressed = isKeyPressed;
        isKeyPressed = CheckClick();
    }

    private bool CheckClick()
    {
        return Keyboard.IsKeyPressed(eventKey);
    }

    public void TryActivateEvent()
    {
        if (!wasKeyPressed && isKeyPressed && onKeyDownAction != null)
        {
            onKeyDownAction.Invoke();
        }
        else if (wasKeyPressed && isKeyPressed && onKeyPressedAction != null)
        {
            onKeyPressedAction.Invoke();
        }
    }
}