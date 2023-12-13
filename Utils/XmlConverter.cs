using System.Xml.Serialization;
using System.Xml;
namespace BankApp.Utils;

public static class XmlConverter
{
    public static string SerializeToXml<T>(T obj)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));

        using (var stringWriter = new StringWriter())
        {
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
            {
                xmlSerializer.Serialize(xmlWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}

