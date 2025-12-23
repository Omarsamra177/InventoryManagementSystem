namespace InventoryManagementSystem.DTOs.Suppliers;

public class UpdateSupplierDto
{
    public string Name { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? Phone { get; set; }
}
