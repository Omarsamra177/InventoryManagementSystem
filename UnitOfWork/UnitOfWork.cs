using InventoryManagementSystem.Data;
using InventoryManagementSystem.Repositories.Interfaces;
using InventoryManagementSystem.Repositories.Implementations;

namespace InventoryManagementSystem.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Categories = new CategoryRepository(context);
        Suppliers = new SupplierRepository(context);
        Products = new ProductRepository(context);
    }

    public ICategoryRepository Categories { get; }
    public ISupplierRepository Suppliers { get; }
    public IProductRepository Products { get; }

    public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();
}
