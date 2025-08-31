using System.Globalization;
using FileConverter.Interfaces;

namespace Services.Readers;

public class TxtFileReader : IFileParser
{
    public async Task<List<Dictionary<string, object>>> Reader(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var records = new List<Dictionary<string, object>>();
        int index = 0;

        string? line; // empty line object
        while ((line = await reader.ReadLineAsync()) != null) // Loop as long as the stream can keep reading a new line
        {
            var row = new Dictionary<string, object>();
            string indexString = index.ToString();
            row.Add(indexString, line); // Each line will have the line index (0 index) followed by the text 
            records.Add(row);
            index++;
        }

        return records;
    }
}