using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils;

namespace Agario.Scripts.Engine.Animations;

public class AnimationsList : IGameUtility
{
    public Dictionary<string, Animation> Animations { get; private set; } = new();

    public AnimationsList()
    {
        Init();
    }
    
    private void Init()
    {
        var animationData = JsonHandler.LoadData<Dictionary<string, AnimationData>>(@"Resources/Files/JSONs/Animations.json");
        
        if (animationData is not null)
        {
            foreach (var (key, data) in animationData)
            {
                Animations[key] = new Animation(data.HasExitTime, data.FrameTime, data.TexturesPathsList);
            }
        }
    }
}