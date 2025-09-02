namespace FileConverter.Interfaces;

public interface IFileWriter
{
    Task Writer(List<Dictionary<string, object>> records, Stream outputStream);
}