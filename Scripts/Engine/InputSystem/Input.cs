using SFML.Window;

namespace Agario.Scripts.Engine.InputSystem;

public static class Input
{
    public static Dictionary<string, EventKey> Keys { get; private set; } = new();

    public static void AddKey(Keyboard.Key key, string name)
    {
        EventKey newKey = new EventKey(key);
        Keys.TryAdd(name, newKey);
    }
    
    public static void AddEventOnDownToKey(Action action, string name)
    {
        Keys[name].SetOnKeyDownEvent(action);
    }
    
    public static void AddEventOnPressedToKey(Action action, string name)
    {
        Keys[name].SetOnKeyPressedEvent(action);
    }

    public static void UpdateKeyStatuses()
    {
        foreach (var key in Keys.Values)
        {
            key.UpdateStatus();
        }
    }
}