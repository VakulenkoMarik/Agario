// ReSharper disable InconsistentNaming
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;

namespace Agario.Scripts.Game;

public enum AnimateObjectType
{
    Player,
}

public struct AnimationsTextures()
{
    private static Texture PlayerIdle1 = new(PathUtils.Get("Resources\\Textures\\PlayerIdle1.png"));
    private static Texture PlayerIdle2 = new(PathUtils.Get("Resources\\Textures\\PlayerIdle2.png"));
    private static Texture PlayerMove1 = new(PathUtils.Get("Resources\\Textures\\PlayerMove1.png"));
    private static Texture PlayerMove2 = new(PathUtils.Get("Resources\\Textures\\PlayerMove2.png"));
    
    public readonly List<Texture> PlayerIdle = new()
    {
        PlayerIdle1,
        PlayerIdle1,
        PlayerIdle1,
        PlayerIdle1,
        PlayerIdle1,
        PlayerIdle2,
        PlayerIdle2,
    };
    
    public readonly List<Texture> PlayerMovement = new()
    {
        PlayerMove1,
        PlayerMove2,
    };
}

public static class AnimationsFactory
{
    private static readonly Dictionary<AnimateObjectType, Func<Shape, Animator>> AnimatorCreators = new()
    {
        { AnimateObjectType.Player, CreatePlayerAnimator },
    };

    private static readonly AnimationsTextures texturesLists = new();

    public static Animator CreateAnimator(AnimateObjectType type, Shape targetShape)
    {
        return AnimatorCreators.TryGetValue(type, out var createAnimator)
            ? createAnimator(targetShape)
            : throw new ArgumentException($"No animator found for type {type}");
    }

    private static Animator CreatePlayerAnimator(Shape target)
    {
        Animation idle = new(texturesLists.PlayerIdle, 0.2f);
        State idleState = new(idle, "idle");
        
        Animation move = new(texturesLists.PlayerMovement, 0.2f);
        State moveState = new(move, "movement");
        
        Animator animator = new();
        animator.Init(target, idleState);
        //animator.AddTransition(idleState, moveState, () => moveState.Trigger);
        //animator.AddTransition(moveState, idleState, () => !moveState.Trigger);
        
        return animator;
    }
}