using InventoryManagementSystem.Data;
using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories.Implementations;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Suppliers.AnyAsync(s => s.Id == id);
}
