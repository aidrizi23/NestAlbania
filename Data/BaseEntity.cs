using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public bool isDeleted { get; set; }
    }
}
