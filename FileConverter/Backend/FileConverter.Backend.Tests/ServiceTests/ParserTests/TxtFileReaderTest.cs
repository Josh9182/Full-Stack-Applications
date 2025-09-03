using FileConverter.Interfaces;
using Services.Readers;
using System.Text;
using System.IO;

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

        Assert.Equal(2, result.Count); // Count of records within the Task<List<Dictionary<string, object>>>
        Assert.NotNull(result); // TXT file successfully parsed
    }
}