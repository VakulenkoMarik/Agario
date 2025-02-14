namespace Agario.Scripts.Engine.Animations;

public class State(string name, Animation animation)
{
    public string Name { get; } = name;
    public Animation Animation { get; } = animation;

    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Update() => Animation.Update();
}