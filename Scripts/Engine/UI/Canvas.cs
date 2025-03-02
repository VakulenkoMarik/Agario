using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
using SFML.Graphics;
using TGUI;

namespace Agario.Scripts.Engine.UI;

public class Canvas : IDrawable
{
    private readonly Gui gui;

    public Canvas()
    {
        RenderWindow window = SceneLoader.Window;
        gui = new (window);
        
        SceneLoader.CurrentScene.AddDrawableObject(this);
    }

    public void AddWidget(Widget widget, string name)
        => gui.Add(widget, name);

    public Drawable? GetMesh()
        => null;

    public void Draw()
        => gui.Draw();

    public void Desrtoy()
        => gui.RemoveAllWidgets();
}