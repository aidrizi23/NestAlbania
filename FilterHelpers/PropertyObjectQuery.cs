using NestAlbania.Data.Enums;

namespace NestAlbania.FilterHelpers
{
    public class PropertyObjectQuery
    {
        public string? Name { get; set; } = null;
        public int? Price { get; set; } = null;
        public int? FullArea { get; set; } = null;
        public int? InsideArea { get; set; } = null;
        public int? BedroomCount { get; set; } = null;
        public int? BathroomCount { get; set; } = null;
    }
}
