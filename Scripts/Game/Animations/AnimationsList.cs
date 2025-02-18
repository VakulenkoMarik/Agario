using Agario.Scripts.Engine.Animations;
using Agario.Scripts.Engine.Utils;
using SFML.Graphics;

namespace Agario.Scripts.Game.Animations;

public struct AnimationsList()
{
    private static readonly Texture PlayerIdle1 = new(PathUtils.Get(@"Resources\Textures\PlayerIdle1.png"));
    private static readonly Texture PlayerIdle2 = new(PathUtils.Get(@"Resources\Textures\PlayerIdle2.png"));
    private static readonly Texture PlayerIdle3 = new(PathUtils.Get(@"Resources\Textures\PlayerIdle3.png"));
    
    private static readonly Texture PlayerWalk1 = new(PathUtils.Get(@"Resources\Textures\PlayerWalk1.png"));
    private static readonly Texture PlayerWalk2 = new(PathUtils.Get(@"Resources\Textures\PlayerWalk2.png"));
    
    private static readonly Texture PlayerRun1 = new(PathUtils.Get(@"Resources\Textures\PlayerRun1.png"));
    private static readonly Texture PlayerRun2 = new(PathUtils.Get(@"Resources\Textures\PlayerRun2.png"));
    
    private static readonly Texture PlayerHappy1 = new(PathUtils.Get(@"Resources\Textures\PlayerHappy1.png"));

    public readonly Animation PlayerIdle = new([
        PlayerIdle1,
        PlayerIdle2,
        PlayerIdle3,
        PlayerIdle2,
    ], 0.2f);
    
    public readonly Animation PlayerWalk = new([
        PlayerWalk1,
        PlayerWalk2,
    ], 0.2f);
    
    public readonly Animation PlayerRun = new([
        PlayerRun1,
        PlayerRun2,
    ], 0.2f);
    
    public readonly Animation PlayerHappy = new([
        PlayerHappy1,
        PlayerHappy1,
    ], 2f);
}