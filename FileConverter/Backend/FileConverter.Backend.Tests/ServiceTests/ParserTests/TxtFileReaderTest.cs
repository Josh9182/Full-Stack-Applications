using FileConverter.Interfaces;
using Services.Readers;
using System.Text;
using System.IO;
using Xunit;

namespace ServiceTests.ParserTests;

public class TxtFileReaderTests
{
    [Fact]
    [Trait("Category", "TxtFRT_VI")]
    public async Task TxtFRT_ValidInput()
    {
        var content = "Hello! This is a test!\n New Line.";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var txtReader = new TxtFileReader();

        var result = await txtReader.Reader(stream);

        Assert.NotNull(result); // TXT file successfully parsed
        Assert.Equal(2, result.Count);

        var indexOneDict = result[0];
        Assert.Equal("Hello! This is a test!", indexOneDict["0"]);

        var indexTwoDict = result[1];
        Assert.Equal(" New Line.", indexTwoDict["1"]);

    }
}