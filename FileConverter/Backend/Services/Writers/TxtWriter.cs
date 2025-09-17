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
            string nameKey = string.IsNullOrEmpty(parentKey) ? dict.Key : $"{parentKey}.{dict.Key}"; // format to store parent.child... : value

            if (dict.Value is Dictionary<string, object> nestedDict) // Checking if each dictionary has a nested dictionary as a value
            {
                flattenedDictResult.AddRange(FlattenDict(nestedDict, nameKey)); // Recursive call accessing the nestedDict with the new nameKey
            }
            else if (dict.Value is List<object> list) // Checking if each dictionary has a nested list as a value
            {
                for (int i = 0; i < list.Count; i++) // for loop, storing index in case of duplicate keys for parentKey.childKey
                {
                    var item = list[i]; // Each element gets accounted for 
                    if (item is Dictionary<string, object> listDict)
                    {
                        flattenedDictResult.AddRange(FlattenDict(listDict, nameKey)); // Recursive call 
                    }
                    else // if primitive...
                    {
                        flattenedDictResult.Add($"\"{nameKey}[{i}]\": \"{item}\""); // Add straight to the list of strings with format "nameKey[i]": "item"
                    }
                }
            }
            else // If non-nested value == primitive
            { 
                flattenedDictResult.Add($"\"{nameKey}\": \"{dict.Value}\"");
            }
        }
        return flattenedDictResult;
    }
}