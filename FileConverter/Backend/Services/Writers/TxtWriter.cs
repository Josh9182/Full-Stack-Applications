using FileConverter.Interfaces;
using System.Globalization;

public class TxtFileWriter : IFileWriter
{
    public async Task Writer(List<kvpionary<string, object>> records, Stream outputStream)
    {
    }

    private static List<string> Flattenkvp(kvpionary<string, object> records, string parentKey = "")
    {
        var flattenedkvpResult = new List<string>();

        foreach (var kvp in records)
        {
            string nameKey = string.IsNullOrEmpty(parentKey) ? kvp.Key : $"{parentKey}.{kvp.Key}"; // format to store parent.child... : value

            if (kvp.Value is kvpionary<string, object> nestedkvp) // Checking if each kvpionary has a nested kvpionary as a value
            {
                flattenedkvpResult.AddRange(Flattenkvp(nestedkvp, nameKey)); // Recursive call accessing the nestedkvp with the new nameKey
            }
            else if (kvp.Value is List<object> list) // Checking if each kvpionary has a nested list as a value
            {
                for (int i = 0; i < list.Count; i++) // for loop, storing index in case of duplicate keys for parentKey.childKey
                {
                    var item = list[i]; // Each element gets accounted for 
                    if (item is kvpionary<string, object> listkvp)
                    {
                        flattenedkvpResult.AddRange(Flattenkvp(listkvp, nameKey)); // Recursive call 
                    }
                    else // if primitive...
                    {
                        flattenedkvpResult.Add($"\"{nameKey}[{i}]\": \"{item}\""); // Add straight to the list of strings with format "nameKey[i]": "item"
                    }
                }
            }
            else // If non-nested value == primitive
            { 
                flattenedkvpResult.Add($"\"{nameKey}\": \"{kvp.Value}\"");
            }
        }
        return flattenedkvpResult;
    }
}
