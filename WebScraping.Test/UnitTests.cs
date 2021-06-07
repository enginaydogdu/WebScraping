using HtmlAgilityPack;
using Moq;
using NUnit.Framework;

namespace WebScraping.Test
{
    [TestFixture]
    class UnitTests
    {
        IScrapingService scrapingService;
        Mock<IHelperService> helperServiceMock;
        HtmlDocument doc = new HtmlDocument();

        [SetUp]
        public void Setup()
        {
            helperServiceMock = new Mock<IHelperService>();
            doc.LoadHtml(Resource1.HtmlDocument);
            helperServiceMock.Setup(p => p.ReadSettings(It.IsAny<string>())).Returns(It.IsAny<string>());
            helperServiceMock.Setup(p => p.LoadDocument(It.IsAny<string>())).Returns(doc);
            //helperServiceMock.Setup(p => p.WriteJsonToFile(It.IsAny<string>()));
            scrapingService = new ScrapingService(helperServiceMock.Object);
            

            
        }

        [Test]
        public void ScrapingService_Execute_GetsHtmlDocAndCreateJsonFile()
        {
            scrapingService.Execute();
        }
    }
}
