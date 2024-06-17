﻿using NestAlbania.Data.Enums;
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

        //lidhja me Agjentin, kategorine, city dhe status.

        public Category Category { get; set; }
        public PropertyStatus Status { get; set; }



    }
   
}
