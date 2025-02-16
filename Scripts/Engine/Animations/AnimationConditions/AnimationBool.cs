namespace Agario.Scripts.Engine.Animations.AnimationConditions;

public class AnimationBool(string name) : AnimationCondition(name)
{
    public void SetActive(bool isActive)
        => IsActive = isActive;
}