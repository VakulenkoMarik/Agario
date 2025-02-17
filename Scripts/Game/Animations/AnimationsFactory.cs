// ReSharper disable InconsistentNaming
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Agario.Scripts.Engine.Animations;
using SFML.Graphics;

namespace Agario.Scripts.Game.Animations;

public enum AnimateObjectType
{
    Player,
}

public static class AnimationsFactory
{
    private static readonly AnimationsList animationsList = new();
    private static readonly AnimatorsList animatorsList = new();
    
    private static readonly Dictionary<AnimateObjectType, Func<Shape, Animator>> AnimatorCreators = new()
    {
        { AnimateObjectType.Player, CreatePlayerAnimator },
    };
    
    public static Animator CreateAnimator(AnimateObjectType type, Shape targetShape)
    {
        if (AnimatorCreators.TryGetValue(type, out var createAnimator))
        {
            return createAnimator(targetShape);
        }
        
        throw new ArgumentException($"No animator found for type {type}");
    }

    private static Animator CreatePlayerAnimator(Shape target)
    {
        Animator animator = animatorsList.GetPlayerAnimator();

        animator.Init(target, new(animationsList.PlayerIdle, "idle", false));
        animator.AddState(new(animationsList.PlayerWalk, "walk", false));
        
        animator.CreateTransition("idle", "walk");
        animator.CreateTransition("walk", "idle");
        animator.AddBooleanToTransition("isWalk", true, "idle", "walk");
        animator.AddBooleanToTransition("isWalk", false, "walk", "idle");

        return animator;
    }
}