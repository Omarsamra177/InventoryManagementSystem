using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.Suppliers;

public class UpdateSupplierDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; } = null!;
    public string? Phone { get; set; }
}
