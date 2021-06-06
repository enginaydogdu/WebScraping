using HtmlAgilityPack;
using NUnit.Framework;
using System;
using System.IO;

namespace WebScraping.Test
{
    [TestFixture]
    public class Tests
    {
        private ScrapingService _scrapingService;
        private HelperService _helperService;

        HtmlDocument doc = new HtmlDocument();

        [SetUp]
        public void Setup()
        {
            _helperService = new HelperService();
            _scrapingService = new ScrapingService();
            doc.LoadHtml(Resource1.HtmlFile);
        }

        [Test]
        public void TestHelperService_ReadSettings_ReturnsValue()
        {
            var url = _helperService.ReadSettings("Settings:Url");
            Assert.IsNotNull(url);
        }

        [Test]
        public void TestScrapingService_Execute_ReturnsJsonFile()
        {
            var jsonString =_scrapingService.Execute(doc);
            Assert.IsNotNull(jsonString);
        }

        [Test]
        public void ScrapingService_WriteJsonToFile_CreatesFile()
        {
            
            var result = _scrapingService.Execute(doc);
            _scrapingService.WriteJsonToFile(result);
            Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Json.txt")));
        }
    }
}