using Agario.Scripts.Engine.Utils;
using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class AnimationData
{
    public bool HasExitTime { get; set; }
    public float FrameTime { get; set; }
    public List<string> TexturesPathsList { get; set; } = new();
}


public class Animation
{
    private float elapsedTime;
    private int currentFrame;
    
    public bool HasExitTime { get; set; }
    public float FrameTime { get; private set; }
    
    private readonly List<Texture> frames = new();
    public List<string> TexturesPathsList { get; private set; }

    public Animation(bool hasExitTime, float frameTime, List<string> texturesPaths)
    {
        HasExitTime = hasExitTime;
        FrameTime = frameTime;
        TexturesPathsList = texturesPaths;
        
        LoadTextures();
    }
    
    public Texture GetCurrentFrame()
        => frames[currentFrame];
    
    public bool IsFinished()
        => currentFrame == frames.Count - 1 || elapsedTime >= FrameTime;

    public void LoadTextures()
    {
        frames.Clear();
        
        foreach (var path in TexturesPathsList)
        {
            string fullPath = PathUtils.Get(Path.Combine(@"Resources/Textures/Animations", path));
            
            if (File.Exists(fullPath))
                frames.Add(new Texture(fullPath));
        }
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= FrameTime)
        {
            elapsedTime = 0;

            if (!HasExitTime)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
            }
            else if (currentFrame < frames.Count - 1)
            {
                currentFrame++;
            }
        }
    }

    public void Reset()
    {
        currentFrame = 0;
        elapsedTime = 0;
    }
}