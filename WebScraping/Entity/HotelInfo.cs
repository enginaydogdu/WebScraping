using System.Collections.Generic;

namespace WebScraping.Entity
{
    public class HotelInfo
    {
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string Classification { get; set; }
        public float ReviewPoints { get; set; }
        public int NumberOfReviews { get; set; }
        public string Description { get; set; }
        public List<RoomCategory> RoomCategories { get; set; }

    }
}
