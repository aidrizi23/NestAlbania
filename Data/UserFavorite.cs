using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class UserFavorite : BaseEntity
    {

        [Key]
        public int Id { get; set; } 

        [ForeignKey("User")]
        public string UserId { get; set; } 
        public  ApplicationUser User { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; } 
        public Property Property { get; set; }


    }
}
