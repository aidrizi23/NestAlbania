using System.ComponentModel.DataAnnotations.Schema;
using NestAlbania.Data;
using NestAlbania.Data.Enums;

namespace NestAlbania.Models.DtoForEdit;

public class PropertyForEditDto
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }
    public string? MainImage { get; set; }
    
    public double Price { get; set; }
        
        
    public string? Documentation { get; set; }
    
    public List<string>? OtherImages { get; set; }

        
    public PropertyStatus Status { get; set; }
    
}