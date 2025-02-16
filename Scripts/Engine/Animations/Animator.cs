using Agario.Scripts.Engine.Animations.AnimationConditions;
using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class Animator : IUpdatable
{
    private FiniteStateMachine? fsm;
    private Shape? sprite;

    private readonly List<AnimationTrigger> triggers = new();
    private readonly List<AnimationBool> bools = new();
    
    private readonly Dictionary<State, List<Transition>> combinations = new();

    public void Init(Shape targetSprite, State firsAnim)
    {
        GameLoop.GetInstance().updatableObjects.Add(this);
        
        fsm = new(firsAnim, combinations);

        sprite = targetSprite;
    }
    
    public void Update()
    {
        if (sprite is not null && fsm is not null)
        {
            fsm.Update();
            sprite.Texture = fsm.GetCurrentTexture();

            foreach (var trigger in triggers)
            {
                trigger.Reset();
            }
        }
    }

    public void AddTrigger(string name)
        => triggers.Add(new(name));
    
    public void SetTrigger(string name)
        => FindRequiredCondition(triggers, name)?.Activate();
    
    public void AddBool(string name)
        => bools.Add(new(name));
    
    public void SetBool(string name, bool activate)
        => FindRequiredCondition(bools, name)?.SetActive(activate);
    
    public void AddTriggerToTransition(string triggerName, string stateFrom, string stateTo)
        => AddConditionToTransition(triggerName, stateFrom, stateTo, triggers);

    public void AddBooleanToTransition(string booleanName, string stateFrom, string stateTo)
        => AddConditionToTransition(booleanName, stateFrom, stateTo, bools);
    
    private void AddConditionToTransition<T>(string conditionName, string stateFrom, string stateTo, List<T> conditions) 
        where T : AnimationCondition
    {
        var condition = FindRequiredCondition(conditions, conditionName);
        if (condition is not null)
        {
            FindRequiredTransition(stateFrom, stateTo)?.AddCondition(() => condition.IsActive);
        }
    }

    private Transition? FindRequiredTransition(string stateFrom, string stateTo)
    {
        State? targetState = combinations.Keys.FirstOrDefault(target => target.Name == stateFrom);

        if (targetState is not null)
        {
            return combinations[targetState].FirstOrDefault(transition => transition.TargetState.Name == stateTo);
        }

        return null;
    }

    private T? FindRequiredCondition<T>(List<T> conditions, string targetName) where T : AnimationCondition
    {
        foreach (var target in conditions)
        {
            if (target.Name == targetName)
            {
                return target;
            }
        }

        return null;
    }
    
    public void CreateState(string name, Animation animation)
    {
        State state = new(animation, name);
        List<Transition> transitions = new();
        
        combinations.Add(state, transitions);
    }
    
    public void CreateTransition(string from, string to)
    {
        State? stateFrom = null;
        State? stateTo = null;
        
        foreach (var state in combinations.Keys)
        {
            if (state.Name == from)
            {
                stateFrom = state;
                continue;
            }

            if (state.Name == to)
            {
                stateTo = state;
            }
        }

        if (stateFrom is not null && stateTo is not null)
        {
            combinations[stateFrom].Add(new Transition(stateTo));
        }
    }
}