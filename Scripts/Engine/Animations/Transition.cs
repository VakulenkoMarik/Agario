namespace Agario.Scripts.Engine.Animations;

public class Transition(State targetState, Func<bool> condition)
{
    public Func<bool> Condition { get; } = condition;
    public State TargetState { get; } = targetState;
}