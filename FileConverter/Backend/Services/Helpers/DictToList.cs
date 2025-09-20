using FileConverter.Interfaces;

namespace Services.Helpers;

public class DictToList : IDictToList
{
    public static List<string> FlattenDict(Dictionary<string, object> records, string parentKey = "")
    {
        var flattenedDictResult = new List<string>();

        foreach (var kvp in records)
        {
            string nameKey = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey}.{kvp.Key}"; // format to store parent.child... : value

            if (kvp.Value is Dictionary<string, object> nestedDict) // Checking if each dictionary has a nested dictionary as a value
            {
                flattenedDictResult.AddRange(FlattenDict(nestedDict, nameKey)); // Recursive call accessing the nestedDict with the new nameKey
            }
            else if (kvp.Value is List<object> list) // Checking if each dictionary has a nested list as a value
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
                flattenedDictResult.Add($"\"{nameKey}\": \"{kvp.Value}\"");
            }
        }
        return flattenedDictResult;
    }
}