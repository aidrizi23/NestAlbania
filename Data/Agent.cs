using System.ComponentModel.DataAnnotations;
using NuGet.Common;

namespace NestAlbania.Data
{
    public class Agent : BaseEntity

    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        public string? Image {  get; set; }
       
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public int LicenseNumber { get; set; }
        
        [Required]
        public string Motto { get; set; }
        
        [Required]
        public int YearsOfExeperience { get; set; }

        

        public string? UserId { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string RoleId { get; set; }

        public ICollection<Property>? Properties { get; set;}
        public ICollection<UserFavorite>? UserFavorites { get; set; }

    }
}