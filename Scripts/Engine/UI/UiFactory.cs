using Agario.Scripts.Engine.Scene;
using TGUI;

namespace Agario.Scripts.Engine.UI;

public static class UiFactory
{
    public static Button CreateButton(Vector2f position, Vector2f scale, Action onPresed, string? text = null)
    {
        Button button = new();
        
        button.SetSize(scale);
        
        position = new Vector2f(position.X - scale.X / 2, position.Y - scale.Y / 2);
        button.SetPosition(position);
        
        var id = button.Connect("Clicked", onPresed);

        SceneLoader.AddOnExitSceneAction(()=>button.Disconnect("Clicked", id));
        SceneLoader.AddOnExitSceneAction(button.Dispose);

        if (!string.IsNullOrEmpty(text))
            button.Text = text;

        return button;
    }
}