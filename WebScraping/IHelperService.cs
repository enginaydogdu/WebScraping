using HtmlAgilityPack;

namespace WebScraping
{
    public interface IHelperService
    {
        string ReadSettings(string key);
        HtmlDocument LoadDocument(string url);
        void WriteJsonToFile(string jsonString);
    }
}