namespace FileConverter.Interfaces;

public interface IFileWriter
{
    Task<MemoryStream> Writer(List<Dictionary<string, object>> records);
}