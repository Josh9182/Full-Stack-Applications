using FileConverter.Interfaces;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Text;
using Services.Readers;

namespace ServiceTests.ParserTests;

public class XmlFileReaderTests
{
    [Fact]
    [Trait("Category", "XmlFRT_VI")]
    public async Task XmlFRT_ValidInput()
    {
        var xmlString = """
            <Shelf Material="Oak" xmlns="http://example.com/default-namespace">
                <Book Author="Josh L.">
                    The Ballad of Josh Lewis
                </Book>
            </Shelf>
        """;

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(xmlString));

        var xmlReader = new XmlFileReader();

        var results = await xmlReader.Reader(ms);

        var dict = results.Single();
        var rootShelf = (Dictionary<string, object>)dict["Shelf"];
        Assert.Equal("Oak", rootShelf["@attribute_Material"]);
        Assert.Equal("http://example.com/default-namespace", rootShelf["@namespace"]);

        var nodeBook = (Dictionary<string, object>)rootShelf["Book"];
        Assert.Equal("Josh L.", nodeBook["@attribute_Author"]);
        Assert.Equal("The Ballad of Josh Lewis", nodeBook["@text"]);

        Assert.Equal(1, results.Count);
        Assert.NotNull(results);
    }
}