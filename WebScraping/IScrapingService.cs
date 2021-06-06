using HtmlAgilityPack;

namespace WebScraping
{
    public interface IScrapingService
    {
        HtmlDocument LoadDocument(string url);
        string Execute(HtmlDocument document);
        void WriteJsonToFile(string jsonString);
    }
}