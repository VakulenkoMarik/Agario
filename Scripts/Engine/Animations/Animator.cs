using Agario.Scripts.Engine.Animations.AnimationConditions;
using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Scene;
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
        SceneLoader.CurrentScene?.AddUpdatableObject(this);
        
        AddState(firsAnim);
        
        fsm = new(firsAnim, combinations);

        sprite = targetSprite;
    }
    
    public void Update()
    {
        fsm?.Update();
        
        if (sprite != null) 
            sprite.Texture = fsm?.GetCurrentTexture();

        foreach (var trigger in triggers)
        {
            trigger.Reset();
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
        => AddConditionToTransition(triggerName, true, stateFrom, stateTo, triggers);

    public void AddBooleanToTransition(string booleanName, bool active, string stateFrom, string stateTo)
        => AddConditionToTransition(booleanName, active, stateFrom, stateTo, bools);

    public void AddBooleansToTransition(bool active, string stateFrom, string stateTo, params string[] booleans)
    {
        foreach (var booleanName in booleans)
        {
            if (!string.IsNullOrEmpty(booleanName))
            {
                AddConditionToTransition(booleanName, active, stateFrom, stateTo, bools);
            }
        }
    }
    
    private void AddConditionToTransition<T>(string conditionName, bool isActive, string stateFrom, string stateTo, List<T> conditions) 
        where T : AnimationCondition
    {
        var condition = FindRequiredCondition(conditions, conditionName);
        if (condition is not null)
        {
            FindRequiredTransition(stateFrom, stateTo)?.AddCondition(() => condition.IsActive == isActive);
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
    
    public void AddState(State state)
        => combinations.Add(state, new());

    public State? TryGetState(string name)
    {
        foreach (var state in combinations.Keys)
        {
            if (state.Name == name)
            {
                return state;
            }
        }

        return null;
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
            combinations[stateFrom].Add(new Transition(stateFrom, stateTo));
        }
    }
    
    public void CreateDoubleTransition(string state1, string state2, params string[] conditions)
    {
        CreateTransition(state1, state2);
        CreateTransition(state2, state1);

        foreach (var name in conditions)
        {
            if (!string.IsNullOrEmpty(name))
            {
                AddDoubleBooleanToTransition(name, true, state1, state2);
                AddDoubleBooleanToTransition(name, false, state2, state1);
            }
        }
    }

    public void AddDoubleBooleanToTransition(string booleanName, bool active, string state1, string state2)
    {
        AddConditionToTransition(booleanName, active, state1, state2, bools);
        AddConditionToTransition(booleanName, !active, state2, state1, bools);
    }
}