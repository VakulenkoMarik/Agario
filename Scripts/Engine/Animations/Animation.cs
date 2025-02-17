using SFML.Graphics;

namespace Agario.Scripts.Engine.Animations;

public class Animation(List<Texture> frames, float frameTime)
{
    private float elapsedTime;
    private int currentFrame;

    public bool HasExitTime;
    
    public Texture GetCurrentFrame()
        => frames[currentFrame];
    
    public bool IsFinished()
    {
        return currentFrame == frames.Count - 1 || elapsedTime >= frameTime;
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime >= frameTime)
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