using NestAlbania.Controllers;

namespace NestAlbania.Models
{
    public class PropertyStatusModel
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public int Sold { get; set; }
        public int Available { get; set; }
       
        public Dictionary<string, int> SalesByDay { get; set; } // Add this property
    }
}
