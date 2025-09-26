using FileConverter.Interfaces;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Services.Readers;

public class XmlFileWriter : IFileWriter
{
    public async Task Writer(List<Dictionary<string, object>> records, Stream outputStream)
    {
        /* custom sorting alg to iterate through each dict and check for attributes, namespaces, and duplicate keys.
           1. Flatten LOD so it becomes standard key.subkey = value format.
           2. Grab the main key, convert that into the root.
           3. Each dictionary becomes its own XML element.
           4. If a namespace, text, attribute, or cdata is found, add that to the specific XElement. 
           5. If a list element is found, take the list name and create n nodes for each list element.
           6. 

        */
    }
}