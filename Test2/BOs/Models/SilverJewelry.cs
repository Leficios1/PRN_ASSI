using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BOs.Models;

public partial class SilverJewelry
{
    public string SilverJewelryId { get; set; } = null!;


    [RegularExpression(@"^[A-Z][a-zA-Z0-9\s]*$", ErrorMessage = "SilverJewelryName must begin with a capital letter")] 
    public string SilverJewelryName { get; set; } = null!;

    public string? SilverJewelryDescription { get; set; }

    public decimal? MetalWeight { get; set; }

    public decimal? Price { get; set; }

    public int? ProductionYear { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CategoryId { get; set; }


    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Category? Category { get; set; }
}
