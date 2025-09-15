using FileConverter.Interfaces;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml.Linq;
using System.Text;
using Services.Readers;
using Xunit;

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
    
        Assert.NotNull(results);
        var dict = results.Single(); // Check for 1 dictionary

        var rootShelf = (Dictionary<string, object>)dict["Shelf"]; // Cast a dictionary object onto the variable, grabbing the root ("Shelf)
        Assert.Equal("Oak", rootShelf["@attribute_Material"]); 
        Assert.Equal("http://example.com/default-namespace", rootShelf["@namespace"]); // Ensure Value & Key are both correct 

        var nodeBook = (Dictionary<string, object>)rootShelf["Book"]; // Grab the child element node, Book
        Assert.Equal("Josh L.", nodeBook["@attribute_Author"]);
        Assert.Equal("The Ballad of Josh Lewis", nodeBook["@text"]);
    }
}