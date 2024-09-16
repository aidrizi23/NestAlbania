using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NestAlbania.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NestAlbania.Data
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? MainImage { get; set; }
        public int Price { get; set; }
        public int FullArea { get; set; }
        public int InsideArea { get; set; }
         public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public string? Documentation { get; set; }
        public List<string>? OtherImages { get; set; }
        public bool IsFavorite { get; set; }
     
        public Category Category { get; set; }
        public PropertyStatus Status { get; set; }
        public City City { get; set; }


        [ForeignKey("AgentId")]
        public int? AgentId { get; set; }
        public virtual Agent? Agent { get; set; }

        public DateTime PostedOn { get; set; }
        public DateTime? LastEdited { get; set; }
        public DateTime? SoldDate { get; set; }
        public double PricePerMeterSquared  =>  (double)Price / FullArea;
        
        // public bool? isDeleted { get; set; }
        public bool? isSold { get; set; }
        public DateTime PriceChangedDate { get; set; }  
        
        // public bool HasParking { get; set; }
        
    }
   
}
