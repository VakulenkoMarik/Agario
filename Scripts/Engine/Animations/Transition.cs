// ReSharper disable CollectionNeverUpdated.Local

namespace Agario.Scripts.Engine.Animations;

public class Transition(State targetState)
{
    public State TargetState { get; } = targetState;
    private readonly List<Func<bool>> transitionConditions = new();

    public void AddCondition(Func<bool> condition)
        => transitionConditions.Add(condition);

    public bool CanDoTransition()
    {
        return transitionConditions.All(condition => condition());
    }
}