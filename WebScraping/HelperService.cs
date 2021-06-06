using Microsoft.Extensions.Configuration;

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
    }
}
