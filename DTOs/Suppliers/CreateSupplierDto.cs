namespace InventoryManagementSystem.DTOs.Suppliers;

public class CreateSupplierDto
{
    public int Id { get; set; }          
    public string Name { get; set; } = null!;
    public string ContactEmail { get; set; } = null!;
    public string? Phone { get; set; }
}
