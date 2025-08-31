namespace FileConverter.Interfaces;

public interface IFileParser
{
    Task<List<Dictionary<string, object>>> Reader(Stream stream);
}