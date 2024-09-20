using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NestAlbania.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestAlbania.Data
{
    public class Property : BaseEntity
    {
     
        public required string Name { get; set; }
        
        
        public required string Description { get; set; }
        public string? MainImage { get; set; }
        

        public required double Price { get; set; }
        
 
        public required double FullArea { get; set; }
        
        public required double InsideArea { get; set; }
        
        public required int BedroomCount { get; set; }
        
        public required int BathroomCount { get; set; }
        
        
        public string? Documentation { get; set; }
        public List<string>? OtherImages { get; set; }
        public bool IsFavorite { get; set; }
        public int PreviousPrice { get; set; }

        
        public required Category Category { get; set; }
        
        public required PropertyStatus Status { get; set; }

        public required City City { get; set; }
       
        
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }

        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }
        public virtual Agent? Agent { get; set; }


        public required DateTime PostedOn { get; set; }
        
        
        public DateTime? LastEdited { get; set; }
        
        public double PricePerMeterSquared  =>  Price / FullArea;
        

        public required bool IsSold { get; set; }
        public DateTime? SoldDate { get; set; }
        public DateTime PriceChangedDate { get; set; }  
        
        public required int Views { get; set; }
        
        
    }
   
}
