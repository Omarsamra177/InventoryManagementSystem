using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<bool> ExistsAsync(int id);
}
