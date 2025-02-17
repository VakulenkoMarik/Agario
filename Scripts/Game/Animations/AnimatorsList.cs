using Agario.Scripts.Engine.Animations;

namespace Agario.Scripts.Game.Animations;

public struct AnimatorsList
{
    public readonly Animator GetPlayerAnimator()
    {
        Animator playerAnimator = new();
        
        playerAnimator.AddBool("isWalk");
        playerAnimator.AddBool("isRun");
        playerAnimator.AddTrigger("isHappy");

        return playerAnimator;
    }
}