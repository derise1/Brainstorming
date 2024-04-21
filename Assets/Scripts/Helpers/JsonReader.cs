using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class JsonReader
{
    public static List<T> FromJson<T>(string filePath)
    {
        IsFileExists(filePath);

        string jsonText = File.ReadAllText(filePath);

        List<T> questionsTemp = JsonConvert.DeserializeObject<List<T>>(jsonText);
        return questionsTemp;
    }

    public static void SaveToJson<T>(List<T> list, string filePath)
    {
        IsFileExists(filePath);

        if(list.Count > 0)
        {
            string jsonText = JsonConvert.SerializeObject(list, Formatting.Indented);

            File.WriteAllText(filePath, jsonText);
        }
    }
    
    private static void IsFileExists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
    }
}
