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
        animator.AddState(new(animationsList.PlayerRun, "run", false));
        animator.AddState(new(animationsList.PlayerHappy, "happy"));
        
        animator.CreateDoubleTransition("idle", "happy");
        animator.CreateDoubleTransition("walk", "happy");
        animator.CreateDoubleTransition("run", "happy");
        
        animator.AddTriggerToTransition("isHappy", "idle", "happy");
        animator.AddTriggerToTransition("isHappy","walk", "happy");
        animator.AddTriggerToTransition("isHappy","run", "happy");
        
        animator.AddBooleansToTransition(false, "happy", "idle", "isRun", "isWalk");
        animator.AddBooleansToTransition(true, "happy", "run", "isRun", "isWalk");
        animator.AddBooleanToTransition("isWalk", true, "happy", "walk");
        
        animator.CreateDoubleTransition("idle", "walk", "isWalk");
        animator.CreateDoubleTransition("walk", "run", "isRun");
        
        animator.CreateTransition("run", "idle");
        
        animator.AddBooleanToTransition("isRun", false, "run", "idle");
        animator.AddBooleanToTransition("isWalk", false, "run", "idle");

        return animator;
    }
}