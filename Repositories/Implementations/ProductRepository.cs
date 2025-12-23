using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTOs.Products;
using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Repositories.Implementations;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(int id)
        => await _context.Products.AnyAsync(p => p.Id == id);

    public IQueryable<Product> GetFilteredProducts(ProductQueryDto query)
    {
        var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Name))
            products = products.Where(p => p.Name.Contains(query.Name));

        if (query.CategoryId.HasValue)
            products = products.Where(p => p.CategoryId == query.CategoryId);

        if (query.SupplierId.HasValue)
            products = products.Where(p => p.SupplierId == query.SupplierId);

        if (query.MinPrice.HasValue)
            products = products.Where(p => p.Price >= query.MinPrice);

        if (query.MaxPrice.HasValue)
            products = products.Where(p => p.Price <= query.MaxPrice);

        if (query.InStock.HasValue)
            products = query.InStock.Value
                ? products.Where(p => p.StockQuantity > 0)
                : products.Where(p => p.StockQuantity == 0);

        return products;
    }
}
