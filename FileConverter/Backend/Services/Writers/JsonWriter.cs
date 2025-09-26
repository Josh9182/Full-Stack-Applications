using FileConverter.Interfaces;
using System.IO;
using System.Text.Json;

namespace Services.Writers;

public class JsonFileWriter : IFileWriter
{
    public async Task Writer(List<Dictionary<string, object>> records, Stream outputStream)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true // Indent to keep JSON format
        };

        await JsonSerializer.SerializeAsync(outputStream, records, options); // Auto convert, no matter how nested 


    }
}