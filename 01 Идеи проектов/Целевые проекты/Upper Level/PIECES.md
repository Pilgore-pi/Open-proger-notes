
# Составить полный список библиотек, чтобы учесть все лицензии

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
    - TO // returns String_TO_Helper struct
        - AsciiCodes
        - Utf8Codes
        - Utf16Codes
        - Utf32Codes
        
        - Base64Bytes ??
        - AsciiBytes
        - Utf8Bytes
        - Utf16Bytes
        - UTF32Bytes
    - AS
        -  ==Serialization format==
        - JsonString (simple struct, json wrapper around string)
            - TO
                - DataTable
                - XML
                - Yaml
                - Csv
                - Xls
        - XmlString
            - TO
                - Json
                - ...
        - YamlString
            - TO
                - Json
                - ...
        - CsvString
            - TO
                - Json
                - ...
        - DataTable
        


## DBF reading

```cs
OdbcConnection Conn = null;
...
string str = @"SELECT * FROM D:\Work\rrk.dbf";
DataTable dt = new DataTable();
OdbcDataAdapter da = new OdbcDataAdapter(str, Conn);
da.Fill(dt);
dataGridView1.DataSource = dt;


string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=directoryPath;Extended Properties=dBASE IV;User ID=Admin;Password=;";
using (OleDbConnection con = new OleDbConnection(constr))
{
    var sql = "select * from " + fileName;
    OleDbCommand cmd = new OleDbCommand(sql, con);
    con.Open();
    DataSet ds = new DataSet(); ;
    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
    da.Fill(ds);
}
```