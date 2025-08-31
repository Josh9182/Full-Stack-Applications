using CsvHelper;
using CsvHelper.Globalization;
using System.Globalization;
using FileConverter.Interfaces;

namespace Services.Readers;

public class CsvReader : IReader {
    public async Task<List<Dictionary<string, object>>> Reader(Stream stream)
    {
        using (var reader = new StreamReader(strean)) ;
        using (var csvReader = new CsvReader(Reader, new CsvConfiguration(CultureInfo.InvariantCulture) // Initiating CsvReader
        {
            Delimiter = ",",
            TrimOptions = TrimOptions.Trim,
            HasHeaderRecord = true,
            IgnoreBlankLines = true
        }));

        var records = new List<Dictionary<string, object>>();

        await csvReader.ReadAsync();
        csvReader.ReadHeader();

        while (await csvReader.ReadAsync()) // Looping as long the csvReader locates a line
        {
            var row = new Dictionary<string, object>();
            foreach (var header in row)
            {
                row[header] = csvReader.GetField(header);
            }

            records.Add(row);
        }

        return records;

    }
}
