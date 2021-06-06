using Microsoft.Extensions.DependencyInjection;

namespace WebScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            var _serviceProvider = RegisterServices();

            //IConfiguration configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", false, true)
            //    .Build();


            var scrapingService = _serviceProvider.GetService<IScrapingService>();
            var helperService = _serviceProvider.GetService<IHelperService>();

            //var url = configuration.GetValue<string>("Settings:Url");
            var url = helperService.ReadSettings("Settings:Url");// configuration.GetValue<string>("Settings:Url");

            var doc = scrapingService.LoadDocument(url);

            
            var jsonString = scrapingService.Execute(doc);
            scrapingService.WriteJsonToFile(jsonString);

            //File.WriteAllTextAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Json.txt"), jsonString);


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
