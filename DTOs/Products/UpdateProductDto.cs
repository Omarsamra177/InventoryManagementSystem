using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.Products;

public class UpdateProductDto
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int SupplierId { get; set; }
}
