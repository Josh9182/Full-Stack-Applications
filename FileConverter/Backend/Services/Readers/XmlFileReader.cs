using FileConverter.Interfaces;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Services.Readers;

public class XmlFileReader : IFileParser
{
    public async Task<List<Dictionary<string, object>>> Reader(Stream stream)
    {
        var records = new List<Dictionary<string, object>>();

        var doc = XDocument.Load(stream);
        var root = doc.Root;

        var dict = new Dictionary<string, object>();
        dict[root.Name.LocalName] = ElementHandler(root); // Key = root name WITHOUT namespace, value = node passed through helper method

        records.Add(dict);
        return records;
    }

    private static Dictionary<string, object> ElementHandler(XElement xmlNode)
    {
        var records = new Dictionary<string, object>();

        var rootNamespace = xmlNode.Name.NamespaceName;
        records["@namespace"] = rootNamespace;

        foreach (var rootAttributes in xmlNode.Attributes()) // Cannot be duplicated, therefore foreach loop
        {
            if (!string.IsNullOrEmpty(rootAttributes.Name.NamespaceName)) // Check for namespaces (:ns) attached to attributes
            {
                var attributeNamespace = rootAttributes.Name.NamespaceName;
                records["@attribute_" + rootAttributes.Name.LocalName] = new Dictionary<string, object>
                {
                    {"value", rootAttributes.Value},
                    {"@namespace", attributeNamespace}
                };
            }
            else
            { 
                records["@attribute_" + rootAttributes.Name.LocalName] = rootAttributes.Value;   
            }
        }

        var rootText = xmlNode.Nodes().OfType<XText>() // Grabbing all text and storing each element in a list
            .Select(t => t.Value.Trim())
            .Where(t => !string.IsNullOrEmpty(t))
            .ToList();

        if (rootText.Count == 1)
        {
            records["@text"] = rootText[0];
        }
        else if (rootText.Count > 1)
        {
            records["@text"] = rootText;
        }

        var rootCData = xmlNode.Nodes().OfType<XCData>() // Grabbing all CData and storing each element in a list
            .Select(cd => cd.Value)
            .Where(cd => !string.IsNullOrEmpty(cd))
            .ToList();

        if (rootCData.Count == 1)
        {
            records["@cdata"] = rootCData[0];
        }
        else if (rootCData.Count > 1)
        {
            records["@cdata"] = rootCData;    
        }

        var rootComments = xmlNode.Nodes().OfType<XComment>() // Grabbing all comments and storing each element in a list
            .Select(c => c.Value.Trim())
            .Where(c => !string.IsNullOrEmpty(c))
            .ToList();

        if (rootComments.Count == 1)
        {
            records["@comment"] = rootComments[0];
        }

        else if (rootComments.Count > 1)
        { 
            records["@comment"] = rootComments;
        } 

        foreach (var rootChildren in xmlNode.Elements())
        {
            var childDict = ElementHandler(rootChildren); // recursive call
            var childKey = rootChildren.Name.LocalName;

            if (!records.ContainsKey(childKey))
            {
                records[childKey] = childDict; // storing recursive call in the parent directory. 
            }
            else
            {
                if (records[childKey] is List<object> list)
                {
                    list.Add(childDict);
                }
                else
                { /* Convert the dictionary stored at [childKey] into a list,
                     storing itself as well as every other nested child element to avoid key overwriting.
                  */
                    records[childKey] = new List<object> { records[childKey], childDict };
                }
            }
        }
        
        return records;
    }
}