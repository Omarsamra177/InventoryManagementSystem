using InventoryManagementSystem.DTOs.Products;
using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Repositories.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<bool> ExistsAsync(int id);
    IQueryable<Product> GetFilteredProducts(ProductQueryDto query);
}
