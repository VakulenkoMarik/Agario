using Agario.Scripts.Engine.Interfaces;
using Agario.Scripts.Engine.Utils.Extensions;

namespace Agario.Scripts.Engine;

public class PauseActivator : IPauseHandler, IGameUtility
{
    private readonly List<IPauseHandler> handlers = new();
    public bool IsPause { get; private set; }

    public void Register(IPauseHandler hand)
    {
        handlers.Add(hand);
    }
    
    public void UnRegister(IPauseHandler hand)
    {
        handlers.RemoveSwap(hand);
    }

    public void PauseToggle()
    {
        SetPause(!IsPause);
    }
    
    public void SetPause(bool isPause)
    {
        IsPause = isPause;

        foreach (IPauseHandler handler in handlers)
        {
            handler.SetPause(isPause);
        }
    }
}