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
    
    private static void IsFileExists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
    }
}
