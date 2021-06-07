using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebScraping
{
    public class HelperService: IHelperService
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

        public string ReadSettings(string key)
        {
            var url = configuration.GetValue<string>(key);
            return url;

        }

        public HtmlDocument LoadDocument(string url)
        {
            var web = new HtmlWeb();
            return web.Load(url);
        }

        public void WriteJsonToFile(string jsonString)
        {
            File.WriteAllTextAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Json.txt"), jsonString);
        }
    }
}
