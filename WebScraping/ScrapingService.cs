using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WebScraping.Entity;

namespace WebScraping
{
    public class ScrapingService: IScrapingService
    {
        private readonly IHelperService _helperService;
        public ScrapingService(IHelperService helperService)
        {
            _helperService = helperService;
        }
        

        public void Execute()
        {
            var url = _helperService.ReadSettings("Settings:Url");
            var doc = _helperService.LoadDocument(url);

            var rows = doc.DocumentNode.SelectNodes("//*[@id='maxotel_rooms']/tbody/tr").ToList().Where(x => !x.OuterHtml.Contains("extendedRow sold"));

            var roomCategories = new List<RoomCategory>();

            foreach (var row in rows)
            {
                var roomCategory = new RoomCategory();

                var adultIndex = row.ChildNodes[1].InnerText.IndexOf("Max adults:");
                if (adultIndex > 0)
                {
                    roomCategory.MaxAdults = Int16.Parse(row.ChildNodes[1].InnerText.Substring(adultIndex + "Max adults: ".Length, 1));
                }

                var childIndex = row.ChildNodes[1].InnerText.IndexOf("Max children:");
                if (childIndex > 0)
                {
                    roomCategory.MaxChildren = Int16.Parse(row.ChildNodes[1].InnerText.Substring(childIndex + "Max children: ".Length, 1));
                }

                roomCategory.RoomType = row.ChildNodes[3].ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText.Replace("\n", "");

                roomCategories.Add(roomCategory);
            }

            var hotelInfo = new HotelInfo
            {
                HotelName = doc.DocumentNode.SelectNodes("//*[@id='hp_hotel_name']/text()").ToList()[1].InnerText.Replace("\n", ""),
                Address = doc.DocumentNode.SelectNodes("//*[@id='showMap2']/span[1]").First().InnerText.Replace("\n", ""),
                Classification = doc.DocumentNode.SelectNodes("//*[@id='wrap-hotelpage-top']/div[1]/span/span[1]/span/span/span").First().Attributes["aria-label"].Value,
                ReviewPoints = float.Parse(doc.DocumentNode.SelectNodes("//*[@id='js--hp-gallery-scorecard']/a/div/div[1]").First().InnerText),
                NumberOfReviews = Int32.Parse(doc.DocumentNode.SelectNodes("//*[@id='js--hp-gallery-scorecard']/a/div/div[2]/div[2]").First().InnerText.Replace(",", "").Replace("reviews", "")),
                Description = doc.DocumentNode.SelectNodes("//*[@id='property_description_content']").First().InnerText.Replace("\n", ""),
                RoomCategories = roomCategories
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonString = JsonSerializer.Serialize(hotelInfo, options);
            _helperService.WriteJsonToFile(jsonString);
        }
    }
}
