using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using FileConverter.Interfaces;

namespace Services.Readers;

public class CsvFileReader : IFileParser {
    public async Task<List<Dictionary<string, object>>> Reader(Stream stream)
    {
        using var reader = new StreamReader(stream);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture) // Create CsvReader configuration
        {
            Delimiter = ",",
            TrimOptions = TrimOptions.Trim,
            HasHeaderRecord = true,
            IgnoreBlankLines = true
        };

        using var csvReader = new CsvReader(reader, config); 

        var records = new List<Dictionary<string, object>>();

        await csvReader.ReadAsync(); // Move to the next line of the document (header).
        csvReader.ReadHeader(); // Read the first line (header) as the headers.

        while (await csvReader.ReadAsync()) // Looping as long the csvReader locates a line.
        {
            var row = new Dictionary<string, object>();
            foreach (var header in csvReader.HeaderRecord)
            {
                row[header] = csvReader.GetField(header);
            }

            records.Add(row);
        }

        return records;

    }
}
