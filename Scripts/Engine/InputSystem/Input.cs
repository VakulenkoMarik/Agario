using SFML.Window;

namespace Agario.Scripts.Engine.InputSystem;

public static class Input
{
    public static Dictionary<string, EventKey> Keys { get; private set; } = new();

    public static void AddKey(Keyboard.Key key, string name)
        => Keys.TryAdd(name, new EventKey(key));
    
    public static void AddEventOnDownToKey(Action action, string name)
        => Keys[name].SetOnKeyDownEvent(action);
    
    public static void AddEventOnPressedToKey(Action action, string name)
        => Keys[name].SetOnKeyPressedEvent(action);
    
    public static void AddEventOnUpToKey(Action action, string name)
        => Keys[name].SetOnKeyUpEvent(action);
    
    public static void UpdateKeyStatuses()
    {
        foreach (var key in Keys.Values)
        {
            key.UpdateStatus();
        }
    }
    
    public static void RegisterControllerKey(Keyboard.Key eventKey, Action action, string name, bool onKeyPressed = true)
    {
        AddKey(eventKey, name);

        if (onKeyPressed)
        {
            AddEventOnPressedToKey(action, name);
            return;
        }
        
        AddEventOnDownToKey(action, name);
    }
    
    public static void UnRegisterControllerKey(string name)
    {
        Keys.TryGetValue(name, out var value);

        if (value is not null)
        {
            Keys.Remove(name);
        }
    }
}