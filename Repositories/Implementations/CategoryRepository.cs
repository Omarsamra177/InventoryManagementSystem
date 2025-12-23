using InventoryManagementSystem.Data;
using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories.Implementations;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Categories.AnyAsync(c => c.Id == id);
}
