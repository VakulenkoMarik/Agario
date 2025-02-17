// ReSharper disable InconsistentNaming

namespace Agario.Scripts.Engine.Animations;

public sealed class State
{
    public Animation TargetAnimation { get; }
    public string Name { get; }

    public State(Animation targetAnimation, string name, bool hasExitTime = true)
    {
        TargetAnimation = targetAnimation;
        TargetAnimation.HasExitTime = hasExitTime;
        
        Name = name;
    }

    public void OnEnter()
    {
        TargetAnimation.Reset();

        OnEnterAction();
    }
    
    public void OnExit()
    {
        OnExitAction();
    }

    public void Update()
    {
        TargetAnimation.Update();
        
        if (TargetAnimation.IsFinished())
        {
            OnExit();
        }
    }

    private void OnEnterAction() { }
    private void OnExitAction() { }
}