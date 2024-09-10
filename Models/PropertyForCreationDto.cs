
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using System.ComponentModel.DataAnnotations;
namespace NestAlbania.Models
{
    public class PropertyForCreationDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Description field is required")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long")]
        [MaxLength(500, ErrorMessage = "Description must be at most 500 characters long")]
        public string Description { get; set; }
        
        public string? MainImage { get; set; }
        public IFormFile? MainImageFile { get; set; }

        [Required(ErrorMessage = "Price field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int Price { get; set; }
        
        [Required(ErrorMessage = "FullArea field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "FullArea must be greater than 0")]
        public int FullArea { get; set; }
        
        [Required(ErrorMessage = "InsideArea field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "InsideArea must be greater than 0")]
        public int InsideArea { get; set; }
        
        [Required(ErrorMessage = "BedroomCount field is required")]
        public int BedroomCount { get; set; }
        
        [Required(ErrorMessage = "BathroomCount field is required")]
        public int BathroomCount { get; set; }
        
        [Required(ErrorMessage = "Status field is required")]
        public PropertyStatus Status { get; set; }

        
        public string? Documentation { get; set; }
        
        public List<string>? OtherImages { get; set; }
        
        
        [Required(ErrorMessage = "Category field is required")]
        public Category Category { get; set; }
      
        [Required(ErrorMessage = "City field is required")]
        public City SelectedCity { get; set; }
        
    }
}
