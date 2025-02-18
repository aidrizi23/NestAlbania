﻿using System.ComponentModel.DataAnnotations.Schema;
using NestAlbania.Data;
using NestAlbania.Data.Enums;

namespace NestAlbania.Models.DtoForEdit;

public class PropertyForEditDto
{

    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public IFormFile? MainImageFile { get; set; }

    public double Price { get; set; }
        
        
    public string? Documentation { get; set; }
    
    public List<IFormFile>? OtherImages { get; set; }

        
    public PropertyStatus Status { get; set; }
    
}