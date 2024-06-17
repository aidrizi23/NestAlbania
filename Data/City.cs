using NestAlbania.Data.Enums;

namespace NestAlbania.Data
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public City CityEnum { get; set; }

    }
}
//Lidhjen e propertit me inumi qe ke ber vet