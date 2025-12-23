using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Repositories.Interfaces;

public interface ISupplierRepository : IGenericRepository<Supplier>
{
    Task<bool> ExistsAsync(int id);
}
