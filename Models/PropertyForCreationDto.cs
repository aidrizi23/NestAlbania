using NestAlbania.Data;
using NestAlbania.Data.Enums;

namespace NestAlbania.Models
{
    public class PropertyForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? MainImage { get; set; }
        public IFormFile MainImageFile { get; set; }
        public int Price { get; set; }
        public int FullArea { get; set; }
        public int InsideArea { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public PropertyStatus Status { get; set; }


        public string? Documentation { get; set; }

        public List<string>? OtherImages { get; set; }
        public Category Category { get; set; }
      
        public City SelectedCity { get; set; }
    }
}
