
**SUPER CONVERT:**

https://github.com/SDN-X/SuperConvert

Plus YAML convert, similar to typical JSON serialization:

https://github.com/aaubry/YamlDotNet

**XML to JSON**

```cs
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace ConvertXml2Json
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DirectoryPath = @"../../../shanselman-blog/";
            string[] files = Directory.GetFiles(DirectoryPath);

            foreach (string fileName in files)
            {
                ProcessFile(fileName);
            }
        }

        private static void ProcessFile(string fileName)
        {
            if (fileName.Contains("feedback"))
            {
                return;
            }

            string xml = File.ReadAllText(fileName);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            string json = JsonConvert.SerializeXmlNode(doc);

            File.WriteAllText(fileName.Replace(".xml", ".json"), json);
        }
    }
}
```


- STRING
    - TO
        - AsciiCodes
        - Utf8Codes
        - Utf16Codes
        - Utf32Codes
        - AsciiBytes
        - Utf8Bytes
        - Utf16Bytes
        - UTF32Bytes
    - AS
        - JsonString (simple struct, json wrapper around string)
            - TO
                - 
        - XmlString
        - YamlString
        - CsvString
        