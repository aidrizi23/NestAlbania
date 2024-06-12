using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class JobApply
    {
        public int Id {  get; set; }
        
        [Required]
        public string Emri { get; set; }
        [Required]
        public string NrTel { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        public IFormFile Resume { get; set; }

    }
}
