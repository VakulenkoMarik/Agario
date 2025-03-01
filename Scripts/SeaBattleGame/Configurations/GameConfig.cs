namespace Agario.Scripts.SeaBattleGame.Configurations;

public static class GameConfig
{
    public static GameData Data { get; private set; } = null!;

    public static void SetData(GameData data)
    {
        Data = data;
        
        Data.Load();
    }
}