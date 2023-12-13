using HtmlAgilityPack;
namespace BankApp.Utils;

public static class HTMLParser
{
    public static string ExtractPreTagMessage(string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var preNode = doc.DocumentNode.SelectSingleNode("//pre");

        return preNode?.InnerText.Trim();
    }
}

