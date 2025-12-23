using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTOs.Categories;

public class CreateCategoryDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
