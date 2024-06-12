using System.ComponentModel.DataAnnotations;

namespace NestAlbania.Data
{
    public class BaseEntity<TEntity>
    {
        [Key]
        public int Id { get; set; }
    }
}
