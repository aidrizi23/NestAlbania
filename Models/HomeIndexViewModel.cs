using NestAlbania.Data;
using NestAlbania.Data.Enums;

namespace NestAlbania.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<dynamic> GroupedProperties { get; set; }
        public IEnumerable<Property> PriceChangedProperties { get; set; }
        public IEnumerable<Property> SoldProperties { get; set; }
        public Agent TopSellingAgent { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<Property> ReducedPriceProperties { get; set; }
        public List<int> SoldPropertyCounts { get; set; }
        public List<string> SoldPropertyCategories { get; set; }
        public Dictionary<string, int> MonthlySoldProperties { get; set; } // Add this property
    }
}
