using FileConverter.Interfaces;
using Services.Readers;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection.Metadata;
using Xunit;

namespace ServiceTests.ParserTests;

public class JsonFileReaderTests
{
    [Fact]
    [Trait("Category", "JsonFRT_VOI")]
    public async Task JsonRT_ValidInput()
    {
        var content = @"
            {
            ""person"": {
                ""name"": ""Josh"",
                ""age"": 30,
                ""isEmployee"": true,
                ""address"": {
                ""street"": ""123 Main St"",
                ""city"": ""Salt Lake City""
                }
            },
            ""skills"": [""C#"", ""React"", ""SQL""],
            ""manager"": null
            }";

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var jsonReader = new JsonFileReader();

        var result = await jsonReader.Reader(stream);

        foreach (var r in result)
        { 
            Assert.True(r.ContainsKey("person")); // [0] key, with a dictionary as a value, storing 4 key-value pairs, and 2 nested key-value pairs. 
            Assert.True(r.ContainsKey("skills")); // [1] key, storing a value of a list of strings
            Assert.True(r.ContainsKey("manager")); // [2] key, storing null as a value.
        }

        Assert.NotNull(result); 
        Assert.Equal(1, result.Count); // Object {} is the root element, which then becomes the single containerizing dictionary 
    }

    [Fact]
    [Trait("Category", "JsonFRT_VAI")]
    public async Task JsonFRT_ValidInput()
    {
        var content = @"[]";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var jsonReader = new JsonFileReader();

        var result = await jsonReader.Reader(stream);

        foreach (var r in result)
        {
            Assert.True(r.ContainsKey(""));
            Assert.True(r.ContainsValue("")); // Key(s) & Value(s) SHOULD be null since JSON input is empty
        }

        Assert.NotNull(result); // File input successfully accessed, leaving empty object.
    }
}