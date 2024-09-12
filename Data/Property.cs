using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NestAlbania.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestAlbania.Data
{
    public class Property : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        public string? MainImage { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        public double FullArea { get; set; }
        
        [Required]
        public double InsideArea { get; set; }
        
        [Required]
        public int BedroomCount { get; set; }
        
        [Required]
        public int BathroomCount { get; set; }
        
        
        public string? Documentation { get; set; }
        public List<string>? OtherImages { get; set; }
        public bool IsFavorite { get; set; }
        public int PreviousPrice { get; set; }

        
        [Required]
        public Category Category { get; set; }
        
        [Required]
        public PropertyStatus Status { get; set; }
        
        [Required]
        public City City { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }

        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }
        public virtual Agent? Agent { get; set; }

        
        [Required]
        public DateTime PostedOn { get; set; }
        
        
        public DateTime? LastEdited { get; set; }

        [Required]
        public double PricePerMeterSquared  =>  Price / FullArea;
        
        [Required]
        public bool? IsSold { get; set; }
        public DateTime PriceChangedDate { get; set; }  
        
        
    }
   
}
