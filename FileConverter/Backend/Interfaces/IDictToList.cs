namespace FileConverter.Interfaces;

public interface IDictToList
{
    static abstract List<string> FlattenDict(Dictionary<string, object> records, string parentKey = "");
}