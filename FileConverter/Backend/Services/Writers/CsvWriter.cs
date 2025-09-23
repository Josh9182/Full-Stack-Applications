using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using FileConverter.Interfaces;
using Services.Helpers;

namespace Services.Writers;

public class CsvFileWriter : IFileWriter
{
    public async Task Writer(List<Dictionary<string, object>> records, Stream outputStream)
    {
        await using var writer = new StreamWriter(outputStream);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            TrimOptions = TrimOptions.Trim,
            HasHeaderRecord = true,
            IgnoreBlankLines = true
        };

        await using var csvWriter = new CsvWriter(writer, config);

        var csvRecords = new List<Dictionary<string, object>>();

        foreach (var dict in records)
        {
            var flattenedLines = DictToList.FlattenDict(dict);
            var row = new Dictionary<string, object>();

            foreach (var line in flattenedLines)
            {
                var splitByColon = line.Split(":", 2); // Split each line at first ":" to avoid failures with multiple ":"
                var key = splitByColon[0];
                var value = splitByColon[1];

                var lastKey = key.Split(".").Last(); // Grab last element in "key.key.key"
                row[lastKey] = value;
            }

            csvRecords.Add(row);
        }
        await csvWriter.WriteRecordsAsync(csvRecords);
    }
}