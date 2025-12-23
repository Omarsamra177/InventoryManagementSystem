using InventoryManagementSystem.DTOs.Suppliers;
using InventoryManagementSystem.Entities;
using InventoryManagementSystem.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SuppliersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
    {
        var suppliers = await _unitOfWork.Suppliers.GetAllAsync();

        var result = suppliers.Select(s => new SupplierDto
        {
            Id = s.Id,
            Name = s.Name,
            ContactEmail = s.ContactEmail,
            Phone = s.Phone
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierDto>> GetById(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);

        if (supplier == null)
            return NotFound();

        return Ok(new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            ContactEmail = supplier.ContactEmail,
            Phone = supplier.Phone
        });
    }

    [HttpPost]
    public async Task<ActionResult<SupplierDto>> Create(CreateSupplierDto dto)
    {
        if (await _unitOfWork.Suppliers.ExistsAsync(dto.Id))
            return BadRequest("Supplier ID already exists.");

        var supplier = new Supplier
        {
            Id = dto.Id,
            Name = dto.Name,
            ContactEmail = dto.ContactEmail,
            Phone = dto.Phone
        };

        await _unitOfWork.Suppliers.AddAsync(supplier);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, new SupplierDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            ContactEmail = supplier.ContactEmail,
            Phone = supplier.Phone
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);

        if (supplier == null)
            return NotFound();

        supplier.Name = dto.Name;
        supplier.ContactEmail = dto.ContactEmail;
        supplier.Phone = dto.Phone;

        _unitOfWork.Suppliers.Update(supplier);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);

        if (supplier == null)
            return NotFound();

        _unitOfWork.Suppliers.Delete(supplier);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
