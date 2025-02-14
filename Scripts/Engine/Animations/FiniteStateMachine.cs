using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class FiniteStateMachine(State initialState)
{
    private State currentState = initialState;
    private readonly Dictionary<State, List<Transition>> combination = new();

    public void Update()
    {
        List<Transition> currentStateTransitions = combination[currentState];
        
        foreach (var transition in currentStateTransitions)
        {
            if (transition.Condition.Invoke())
            {
                ChangeState(transition.TargetState);
                break;
            }
        }
        
        currentState.Update();
    }
    
    public void AddTransition(State from, State to, Func<bool> condition)
    {
        if (!combination.ContainsKey(from))
            combination[from] = new List<Transition>();

        combination[from].Add(new Transition(to, condition));
    }
    
    private void ChangeState(State newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
    
    public Texture GetCurrentTexture() => currentState.Animation.GetCurrentFrame();
}