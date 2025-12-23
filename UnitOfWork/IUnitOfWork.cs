using InventoryManagementSystem.Repositories.Interfaces;

namespace InventoryManagementSystem.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    ISupplierRepository Suppliers { get; }
    IProductRepository Products { get; }

    Task<int> CompleteAsync();
}
