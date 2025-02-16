// ReSharper disable CollectionNeverQueried.Local

using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class FiniteStateMachine(State initialState, Dictionary<State, List<Transition>> combinations)
{
    private State currentState = initialState;

    public void Update()
    {
        List<Transition> currentStateTransitions = combinations[currentState];
        
        foreach (var transition in currentStateTransitions)
        {
            if (transition.CanDoTransition())
            {
                DoTransition(transition.TargetState);
                break;
            }
        }
        
        currentState.Update();
    }
    
    private void DoTransition(State toState)
        => ChangeState(toState);
    
    private void ChangeState(State newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
    
    public Texture GetCurrentTexture()
        => currentState.Animation.GetCurrentFrame();
}