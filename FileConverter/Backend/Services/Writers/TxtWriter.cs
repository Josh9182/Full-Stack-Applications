using FileConverter.Interfaces;
using Services.Helpers;
using System.Globalization;

public class TxtFileWriter : IFileWriter
{
    public async Task Writer(List<Dictionary<string, object>> records, Stream outputStream)
    {
        using (var writer = new StreamWriter(outputStream, leaveOpen: true)) /* Registers StreamWriter, writing to the
                                                                                outputStream param, left open so the stream
                                                                                can continuously be accessed.
                                                                             */
        {
            foreach (var dict in records)
            {
                var flattenedList = DictToList.FlattenDict(dict);
                writer.WriteLine(flattenedList);
            }
        }
        outputStream.Position = 0; // Resets the stream position to be reused

    }
}