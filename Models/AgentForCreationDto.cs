using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Models
{
    public class AgentForCreationDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(50, ErrorMessage = "Name must be at most 50 characters long")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Surname field is required")]
        [MinLength(3, ErrorMessage = "Surname must be at least 3 characters long")]
        [MaxLength(20, ErrorMessage = "Surname must be at most 50 characters long")]
        public string Surname { get; set; }
        
        // per momentin nuk do te jete required sepse file handling nuk eshte implementuar ende sakte
        public IFormFile? Image { get; set; }
        

        [Required(ErrorMessage = "PhoneNumber field is required")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "License Number field is required")]
        public int LicenseNumber { get; set; }
        
        [Required(ErrorMessage = "Motto field is required")]
        public string Motto { get; set; }   
        
        [Required(ErrorMessage = "Years of experience field is required")]
        [Range(1, 100, ErrorMessage = "Years of experience must be between 1 and 100")]
        public int YearsOfExeperience { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string RoleId = "a14bs9c0-aa65-4af8-bd17-00bd9344e575"; // Agent role id
        
        public int? PropertyId { get; set; } // mduket se nuk e kan perdor fare 
        
    }
}
