#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using Agario.Scripts.SeaBattleGame.GameObjects;

namespace Agario.Scripts.SeaBattleGame.Controllers;

public class Controller
{
    public bool Attacks;
    
    public Player TargetPlayer { get; private set; }

    public void SetPlayer(Player player)
        => TargetPlayer = player;

    public void Attack()
    {
        Attacks = true;
        
        AttackProcessing();
    }
    
    protected virtual void AttackProcessing() { }

    protected void EndAttack()
        => Attacks = false;

    public bool IsDefeat()
        => TargetPlayer.ShipsCount <= 0;
}