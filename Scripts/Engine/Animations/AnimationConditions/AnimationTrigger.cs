namespace Agario.Scripts.Engine.Animations.AnimationConditions;

public class AnimationTrigger(string name) : AnimationCondition(name)
{
    public void Reset()
        => IsActive = false;
    
    public void Activate()
        => IsActive = true;
}