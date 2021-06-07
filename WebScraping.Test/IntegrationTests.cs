using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace WebScraping.Test
{
    [TestFixture]
    public class IntegrationTests
    {
        private IScrapingService _scrapingService;
        private IHelperService _helperService;
        Mock<IScrapingService> scrapingServiceMock;
        HtmlDocument doc = new HtmlDocument();

        [SetUp]
        public void Setup()
        {
            //scrapingServiceMock.Setup(p => p.Execute(It.IsAny<HtmlDocument>())).Returns(Resource1.JsonString);
            //scrapingServiceMock = new Mock<IScrapingService>();
            _helperService = new HelperService();
            _scrapingService = new ScrapingService(_helperService);
            doc.LoadHtml(Resource1.HtmlDocument);
        }

        [Test]
        public void HelperService_ReadSettings_ReturnsValue()
        {
            var url = _helperService.ReadSettings("Settings:Url");
            Assert.IsNotNull(url);
        }

        [Test]
        public void HelperService_LoadDocumentFromWeb_ReturnsHtmlDoc()
        {
            var htmlDoc = _helperService.LoadDocument(_helperService.ReadSettings("Settings:Url"));
            Assert.IsNotNull(htmlDoc);
        }

        [Test]
        public void HelperService_WriteJsonToFile_CreatesFile()
        {
            _helperService.WriteJsonToFile(Resource1.JsonString);
            Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Json.txt")));
        }

        [Test]
        public void ScrapingService_Execute_GetsHtmlDocAndCreateJsonFile()
        {
            _scrapingService.Execute();
            Assert.IsTrue(File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Json.txt")));
        }

        

        
    }
}