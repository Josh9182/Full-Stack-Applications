using FileConverter.Interfaces;
using System.Globalization;

public class TxtFileWriter : IFileWriter
{
    public async Task Writer(List<Dictionary<string, object>> records, Stream outputStream)
    {
    }

    private static List<string> FlattenDict(Dictionary<string, object> records, string parentKey = "")
    {
        var flattenedDictResult = new List<string>();

        foreach (var dict in records)
        {
            string nameKey = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey}.{kvp.Key}";

            if (kvp.Value is Dictionary<string, object> nestedDict)
            {
                flattenedDictResult.AddRange(FlattenDict(nestedDict, parentKey)); 
            }
        }   
    }
}