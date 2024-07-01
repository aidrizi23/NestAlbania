using NestAlbania.Data;
using NestAlbania.Data.Enums;

namespace NestAlbania.FilterHelpers
{
    public class PropertyObjectQuery
    {
        public string? Name { get; set; } = null;
       
        public int? FullArea { get; set; } = null;
        public int? InsideArea { get; set; } = null;
        public int? BedroomCount { get; set; } = null;
        public int? BathroomCount { get; set; } = null;
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public string? AgentName { get; set; } = null;
        
    }
}
