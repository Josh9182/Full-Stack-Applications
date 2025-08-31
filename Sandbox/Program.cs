using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

var records = new List<Dictionary<string, object>>();
        using var reader = new StreamReader("input.csv");
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ",",
            IgnoreBlankLines = true,
            TrimOptions = TrimOptions.Trim
        });

        await csvReader.ReadAsync(); // Move to the first row of the CSV File, DOESNT READ JUST ADVANCES
        csvReader.ReadHeader(); // Reads the first row, treats this row as the header row

        while (await csvReader.ReadAsync()) // As long as the csvReader can keep advancing to the next line...
        {
            var eachLineDict = new Dictionary<string, object>();
            foreach (var header in csvReader.HeaderRecord)
            {
                eachLineDict[header] = csvReader.GetField(header);
            }
            records.Add(eachLineDict);
        }
foreach (var record in records)
{
    foreach (var r in record)
    {
        Console.Write($"{r.Key}, {r.Value}");
     }
        }