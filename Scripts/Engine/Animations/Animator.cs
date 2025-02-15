using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class Animator : IUpdatable
{
    private FiniteStateMachine? fsm;
    private Shape? sprite;

    private readonly List<State> states = new();

    public void Init(Shape targetSprite, State firsAnim)
    {
        GameLoop.GetInstance().updatableObjects.Add(this);
        
        fsm = new(firsAnim);
        states.Add(firsAnim);

        sprite = targetSprite;
    }

    public void SetTrigger(string name)
    {
        State? targetState = null;
        
        foreach (var state in states)
        {
            if (state.Name == name)
            {
                targetState = state;
            }
        }
        
        targetState?.SetTrigger();
    }

    public void AddTransition(State from, State to, Func<bool> condition)
    {
        fsm?.AddTransition(from, to, condition);

        if (!states.Contains(to))
        {
            states.Add(to);
        }
    }
    
    public void Update()
    {
        if (sprite is null || fsm is null)
        {
            return;
        }
        
        fsm.Update();
        sprite.Texture = fsm.GetCurrentTexture();
    }
}