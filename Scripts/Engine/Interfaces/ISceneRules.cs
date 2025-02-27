namespace Agario.Scripts.Engine.Interfaces;

public interface ISceneRules
{
    public void Start();
    public void OnEnd() {}
    public void Init() {}
    public void Update() {}
}