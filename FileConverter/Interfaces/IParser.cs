namespace FileConverter.Interfaces;

public interface IParser
{
    Task<MemoryStream> Writer(List<Dictionary<string, object>> records);
}