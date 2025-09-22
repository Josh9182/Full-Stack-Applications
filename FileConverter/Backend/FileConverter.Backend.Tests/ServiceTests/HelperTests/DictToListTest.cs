using Xunit;
using FileConverter.Interfaces;
using Services.Helpers;

namespace ServiceTests.HelperTests;

public class DictToListTests
{
    [Fact]
    [Trait("Category", "DTL_VI")]
    public async Task DictToList_ValidInput()
    {
        var dictionary = new Dictionary<string, object>
        {
            {"Name", "Josh"}, {"Information",
                new Dictionary<string, object> {
                    {"Age", 20},
                } }
        };

        var result = DictToList.FlattenDict(dictionary);

        Assert.NotNull(result); // CSV successfully parsed

        var flattenedFirstElement = result[0];
        Assert.Equal("\"Name\": \"Josh\"", flattenedFirstElement);

       var flattenedSecondElement = result[1];
       Assert.Equal("\"Information.Age\": \"20\"", flattenedSecondElement);

    }
}