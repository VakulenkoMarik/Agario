using System.Text.Json;

namespace Agario.Scripts.Engine.Utils;

public static class JsonHandler
{
    public static T? LoadData<T>(string filePath)
    {
        if (!File.Exists(PathUtils.Get(filePath)))
        {
            Console.WriteLine("ERROR!!!");
            return default;
        }

        try
        {
            string json = File.ReadAllText(PathUtils.Get(filePath));
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON exeption: {ex.Message}");
            return default;
        }
    }
    
    public static void SaveData<T>(T data, string filePath)
    {
        try
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(PathUtils.Get(filePath), json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON exeption: {ex.Message}");
        }
    }
}