// ReSharper disable InconsistentNaming
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Agario.Scripts.Engine;
using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Game.GameObjects;
using SFML.Graphics;

namespace Agario.Scripts.Game.Animations;

public enum AnimateObjectType
{
    Player,
}

public static class AnimatorsFactory
{
    private static readonly AnimatorsList animatorsList = new();
    
    private static readonly Dictionary<AnimateObjectType, Func<GameObject, Animator>> AnimatorCreators = new()
    {
        { AnimateObjectType.Player, CreateCharacterAnimator },
    };
    
    public static Animator CreateAnimator(AnimateObjectType type, GameObject targetShape)
    {
        if (AnimatorCreators.TryGetValue(type, out var createAnimator))
        {
            return createAnimator(targetShape);
        }
        
        throw new ArgumentException($"No animator found for type {type}");
    }

    private static Animator CreateCharacterAnimator(GameObject gameObject)
    {
        Animator animator = animatorsList.GetPlayerAnimator();
        
        if (gameObject is Player player)
        {
            Shape shape = (Shape)gameObject.GetMesh();
            Dictionary<string, Animation> animations = player.Character.Animations;
            
            return player.Character.Name switch
            {
                "Boba" => CreatePlayerAnimator(animator, shape, animations["BobaIdle"], animations["BobaWalk"],
                    animations["BobaRun"], animations["BobaHappy"]),
                "Biba" => CreatePlayerAnimator(animator, shape, animations["BibaIdle"], animations["BibaWalk"],
                    animations["BibaRun"], animations["BibaHappy"]),
                _ => throw new Exception("Character is not registered")
            };
        }

        throw new ArgumentException("Game object is not player");
    }

    private static Animator CreatePlayerAnimator(Animator animator, Shape target, Animation idle,
        Animation walk, Animation run, Animation happy)
    {
        animator.Init(target, new(idle, "idle", false));
        animator.AddState(new(walk, "walk", false));
        animator.AddState(new(run, "run", false));
        animator.AddState(new(happy, "happy"));
        
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