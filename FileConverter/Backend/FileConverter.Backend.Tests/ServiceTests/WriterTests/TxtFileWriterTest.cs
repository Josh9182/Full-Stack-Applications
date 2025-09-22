using FileConverter.Interfaces;
using Services.Readers;
using System.Text;
using System.IO;
using Xunit;
using Xunit;
using Services.Helpers;
using System.Globalization;
using Services.Writers;
using ServiceTests.HelperTests;
using ServiceTests.ParserTests;

public class TxtFileWriterTests
{
    [Fact]
    [Trait("Category", "TxtFWT_VI")]
    public async Task TxtFWT_ValidInput()
    {
        var xmlContent = new List<Dictionary<string, object>>
        {
            new Dictionary<string, object>
            {
                { "user", new Dictionary<string, object>
                    {
                        { "profile", new Dictionary<string, object>
                            {
                                { "name", "Josh" },
                                { "role", "student" }
                            }
                        },
                        { "age", 25 }
                    }
                }
            },
            new Dictionary<string, object> {
                { "Information", new Dictionary<string, object>
                    {
                        { "Status", new Dictionary<string, object>
                            {
                                { "IsEmployed", "No" }
                            }
                        },
                        { "Language", "English" }
                    }
                }
            }
        };
        
        using var stream = new MemoryStream();
        var txtWriter = new TxtFileWriter();

        var result = txtWriter.Writer(xmlContent, stream);

        using var reader = new StreamReader(stream);
        var readerResult = reader.ReadToEnd();

        Assert.NotNull(readerResult);

    }
}