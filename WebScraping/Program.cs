using Microsoft.Extensions.DependencyInjection;

namespace WebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            var _serviceProvider = RegisterServices();

            var scrapingService = _serviceProvider.GetService<IScrapingService>();
            var helperService = _serviceProvider.GetService<IHelperService>();

            var url = helperService.ReadSettings("Settings:Url");

            var doc = scrapingService.LoadDocument(url);
            
            var jsonString = scrapingService.Execute(doc);
            scrapingService.WriteJsonToFile(jsonString);
        }

        private static ServiceProvider RegisterServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IScrapingService, ScrapingService>()
                .AddSingleton<IHelperService, HelperService>()
                .BuildServiceProvider();            

            return serviceProvider;
        }

    }
}
