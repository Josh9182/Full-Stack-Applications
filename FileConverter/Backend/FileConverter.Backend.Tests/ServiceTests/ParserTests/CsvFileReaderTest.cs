using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using FileConverter.Interfaces;
using Services.Readers;
using System.Text;
using System.Text;
using System.IO;
using System.Linq;

namespace ServiceTests.ParserTests;
public class CsvFileReaderTests
{
    [Fact]
    [Trait("Category", "CsvFRT_VI")]
    public async Task CsvFRT_ValidInput()
    {
        var content = "Hello!\nGoodbye!\nHello!";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var csvReader = new CsvFileReader();

        var result = await csvReader.Reader(stream);

        var resultCount = result.Sum(d => d.Count);

        Assert.Equal(2, resultCount); // Count of records within the Task<List<Dictionary<string, object>>>
        Assert.NotNull(result); // CSV successfully parsed
    }
}