using Agario.Scripts.Engine.Interfaces;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class Animator : IUpdatable
{
    private FiniteStateMachine? fsm;
    private Shape? sprite;

    public void Init(Shape targetSprite, Animation firsAnim, string animName)
    {
        State state = new(animName, firsAnim);
        fsm = new(state);

        sprite = targetSprite;
    }

    public void AddTransition(State from, State to, Func<bool> condition)
        => fsm?.AddTransition(from, to, condition);
    
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