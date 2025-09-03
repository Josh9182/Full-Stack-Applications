using FileConverter.Interfaces;
using System.IO;
using System.Text.Json;

namespace Services.Readers;

public class JsonFileReader : IFileParser
{
    public async Task<List<Dictionary<string, object>>> Reader(Stream stream)
    {
        var records = new List<Dictionary<string, object>>();
        using var jsonReader = await JsonDocument.ParseAsync(stream);

        switch (jsonReader.RootElement.ValueKind) // Switch, grabbing the first parsed JSON element's value type
        {
            case JsonValueKind.Array: // If container == Array...
                foreach (var item in jsonReader.RootElement.EnumerateArray()) // Loop for each item in the JSON array
                {
                    if (item.ValueKind == JsonValueKind.Object) /* If the array has a nested object, 
                                                                   dissect the oject's values. 
                                                                */
                    {
                        records.Add(ValueProcessor(item));
                    }
                    else
                    {
                        records.Add(new Dictionary<string, object> /* if != object, add a default key and then 
                                                                      dissect the array's values via the Handler
                                                                      as opposed to the Processor.
                                                                   */
                        {
                            {"value", ValueKindHandler(item)}
                        });
                    }
                }
                break;

            case JsonValueKind.Object: // If container == Object...
                records.Add(ValueProcessor(jsonReader.RootElement)); // Process the object as a whole and store the result
                break;

            default:
                throw new InvalidDataException($"Root JSON must be an object or array of objects/primitives.");
        }

        return records;

    }

    private static Dictionary<string, object> ValueProcessor(JsonElement element) // JSON object exclusive.
    {
        var dict = new Dictionary<string, object>(); // Every JsonElement will have a Dictionary associated with it.

        foreach (var value in element.EnumerateObject())
        {
            dict[value.Name] = ValueKindHandler(value.Value); 
        }

        return dict;

    }

    private static object ValueKindHandler(JsonElement element) {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object: // {}
                return ValueProcessor(element); /* As long as an Object is found, 
                                                    recursively shove it through the ValueProcessor 
                                                    until its values are organized correctly. 
                                                 */

            case JsonValueKind.Array: // []
                var list = new List<object>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(ValueKindHandler(item)); // Iterate throughout the array until its values are organized correctly.
                }
                return list;

            case JsonValueKind.String:
                return element.GetString();

            case JsonValueKind.Number:
                if (element.TryGetInt64(out var num)) // if Number == Int64, return number as integer.
                {
                    return num;
                }
                else
                { 
                    return element.GetDouble(); // Fallback, return as double.
                }

            case JsonValueKind.True:
                return true;

            case JsonValueKind.False:
                return false;

            case JsonValueKind.Null:
                return null;

            default:
                throw new InvalidDataException(
                    $"Unexpected value kind: {element}\n" + 
                    "Must abide by: Object, Array, String, Number, Boolean, or Null.\n" +
                    "Please Try again.");
        }
     }

}