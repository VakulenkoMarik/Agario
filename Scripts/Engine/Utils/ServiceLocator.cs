using Agario.Scripts.Engine.Interfaces;

namespace Agario.Scripts.Engine.Utils;

public class ServiceLocator
{
    public static ServiceLocator Instance => _instance ??= new ServiceLocator();
    private static ServiceLocator? _instance;

    private readonly List<IGameUtility> utilities = new();
    
    private ServiceLocator() { }

    public void Register(IGameUtility utility)
    {
        if (!utilities.Contains(utility))
        {
            utilities.Add(utility);
        }
    }

    public T? TryGet<T>() where T : IGameUtility
    {
        foreach (var utility in utilities)
        {
            if (utility is T gameUtility)
            {
                return gameUtility;
            }
        }

        return default;
    }
    
    public T Get<T>() where T : IGameUtility
    {
        return TryGet<T>() ?? throw new InvalidOperationException($"Service {typeof(T).Name} is not registered.");
    }
    
    public void Unregister<T>() where T : IGameUtility
    {
        utilities.RemoveAll(u => u is T);
    }
}