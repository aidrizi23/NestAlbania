using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class UserFavorite : BaseEntity
    {

        [Key]
        public int Id { get; set; } 

        [ForeignKey("UserId")]
        public string UserId { get; set; } 
        public  ApplicationUser User { get; set; }

        [ForeignKey("PropertyId")]
        public int PropertyId { get; set; } 
        public Property Property { get; set; }
      
        //[ForeignKey("AgentId")]
        //public int AgentId { get; set; }
        //public  Agent Agent{ get; set; }


    }
}
