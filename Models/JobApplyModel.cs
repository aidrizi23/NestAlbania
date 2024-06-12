using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Models
{
    public class JobApplyModel
    {

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
