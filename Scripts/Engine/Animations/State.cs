namespace Agario.Scripts.Engine.Animations;

public class State(Animation animation, string name)
{
    public Animation Animation { get; } = animation;
    public string Name { get; } = name;

    public void OnEnter()
    {
        Animation.Reset();

        OnEnterAction();
    }
    
    public void OnExit()
    {
        OnExitAction();
    }

    public void Update()
    {
        Animation.Update();
        
        if (Animation.IsFinished())
        {
            OnExit();
        }
    }
        
    protected virtual void OnEnterAction() { }
    protected virtual void OnExitAction() { }
}