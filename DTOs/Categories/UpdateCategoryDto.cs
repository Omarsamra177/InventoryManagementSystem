using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.Categories;

public class UpdateCategoryDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
