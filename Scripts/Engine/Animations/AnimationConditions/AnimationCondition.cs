namespace Agario.Scripts.Engine.Animations.AnimationConditions;

public abstract class AnimationCondition(string name)
{
    public readonly string Name = name;
    public bool IsActive { get; protected set; }
}