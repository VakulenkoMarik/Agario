namespace Agario.Scripts.Engine.Animations;

public class State(Animation animation, string name)
{
    public Animation Animation { get; } = animation;
    public string Name { get; } = name;

    public bool Trigger { get; private set; }

    public void SetTrigger()
        => Trigger = true;

    public void OnEnter()
    {
        Animation.Reset();

        OnEnterAction();
    }
    
    public void OnExit()
    {
        Trigger = false;

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