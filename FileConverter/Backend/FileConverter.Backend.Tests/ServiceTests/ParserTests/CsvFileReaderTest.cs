using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using FileConverter.Interfaces;
using Services.Readers;
using System.Text;
using System.IO;
using System.Linq;
using Xunit;

namespace ServiceTests.ParserTests;

public class CsvFileReaderTests
{
    [Fact]
    [Trait("Category", "CsvFRT_VI")]
    public async Task CsvFRT_ValidInput()
    {
        var content = "Good Afternoon!\nGood Evening!\nGood Night!";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var csvReader = new CsvFileReader();

        var result = await csvReader.Reader(stream);

        Assert.NotNull(result); // CSV successfully parsed

        var firstDict = result[0];
        Assert.Equal("Good Evening!", firstDict["Good Afternoon!"]);

        var secondDict = result[1];
        Assert.Equal("Good Night!", secondDict["Good Afternoon!"]);
    }
}