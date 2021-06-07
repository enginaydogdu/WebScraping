using Microsoft.Extensions.DependencyInjection;

namespace WebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            var _serviceProvider = RegisterServices();

            var scrapingService = _serviceProvider.GetService<IScrapingService>();
            scrapingService.Execute();
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
