namespace FileConverter.Interfaces;

public interface IParser
{
    Task<List<Dictionary<string, object>>> Reader(Stream stream);
}