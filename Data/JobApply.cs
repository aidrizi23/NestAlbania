using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class JobApply
    {
        public int Id {  get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }


    }
}
