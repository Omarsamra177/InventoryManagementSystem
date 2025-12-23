using InventoryManagementSystem.DTOs.Products;
using InventoryManagementSystem.Entities;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] ProductQueryDto query)
    {
        var productsQuery = _unitOfWork.Products.GetFilteredProducts(query);

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            productsQuery = query.SortBy.ToLower() switch
            {
                "name" => query.Desc ? productsQuery.OrderByDescending(p => p.Name) : productsQuery.OrderBy(p => p.Name),
                "price" => query.Desc ? productsQuery.OrderByDescending(p => p.Price) : productsQuery.OrderBy(p => p.Price),
                "stock" => query.Desc ? productsQuery.OrderByDescending(p => p.StockQuantity) : productsQuery.OrderBy(p => p.StockQuantity),
                _ => productsQuery
            };
        }

        var page = query.Page < 1 ? 1 : query.Page;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var products = await productsQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryName = p.Category.Name,
                SupplierName = p.Supplier.Name
            })
            .ToListAsync(); 

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryName = product.Category.Name,
            SupplierName = product.Supplier.Name
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        if (await _unitOfWork.Products.ExistsAsync(dto.Id))
            return BadRequest("Product ID already exists.");

        if (!await _unitOfWork.Categories.ExistsAsync(dto.CategoryId))
            return BadRequest("Category does not exist.");

        if (!await _unitOfWork.Suppliers.ExistsAsync(dto.SupplierId))
            return BadRequest("Supplier does not exist.");

        var product = new Product
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId,
            SupplierId = dto.SupplierId
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        if (!await _unitOfWork.Categories.ExistsAsync(dto.CategoryId))
            return BadRequest("Category does not exist.");

        if (!await _unitOfWork.Suppliers.ExistsAsync(dto.SupplierId))
            return BadRequest("Supplier does not exist.");

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.CategoryId = dto.CategoryId;
        product.SupplierId = dto.SupplierId;

        _unitOfWork.Products.Update(product);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
