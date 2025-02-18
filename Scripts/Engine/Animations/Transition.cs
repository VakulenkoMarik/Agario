// ReSharper disable CollectionNeverUpdated.Local

namespace Agario.Scripts.Engine.Animations;

public class Transition(State from, State targetState)
{
    private State StateFrom { get; } = from;
    public State TargetState { get; } = targetState;
    private readonly List<Func<bool>> transitionConditions = new();

    public void AddCondition(Func<bool> condition)
        => transitionConditions.Add(condition);

    public bool CanDoTransition()
    {
        Animation animation = StateFrom.TargetAnimation;
        
        if (animation.HasExitTime && !animation.IsFinished())
            return false;
        
        if (transitionConditions.Count == 0)
            return false;
        
        return transitionConditions.All(condition => condition());
    }
}