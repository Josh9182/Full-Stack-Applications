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

        Assert.NotNull(result);
        Assert.Equal(1, result.Count); // Object {} is the root element, which then becomes the single containerizing dictionary 

        var dict = result.Single();
        var person = (Dictionary<string, object>)dict["person"]; // First key
        Assert.Equal("Josh", person["name"]);
        Assert.Equal(30, person["age"]);
        Assert.Equal(true, person["isEmployee"]);  // Checking all primitive values

        var address = (Dictionary<string, object>)person["address"]; // Accessing sub object
        Assert.Equal("123 Main St", address["street"]);
        Assert.Equal("Salt Lake City", address["city"]);

        var skills = (List<object>)dict["skills"]; 
        Assert.Equal("C#", skills[0]);
        Assert.Equal("React", skills[1]);
        Assert.Equal("SQL", skills[2]);

        var manager = (Dictionary<string, object>)dict["manager"];
        Assert.Null(manager);

    }

    [Fact]
    [Trait("Category", "JsonFRT_VAI")]
    public async Task JsonFRT_ValidInput()
    {
        var content = @"[]";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

        var jsonReader = new JsonFileReader();

        var result = await jsonReader.Reader(stream);

        Assert.NotNull(result); // File input successfully accessed, leaving empty object.
        Assert.Empty(result);
    }
}