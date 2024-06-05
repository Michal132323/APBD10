using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using solution.DataBase;
using solution.DbContext;
using solution.DTOs;

namespace solution.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly MyDbContext _context;

    public ProductsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest();
        }

        var product = new Product
        {
            Name = productDto.ProductName,
            Weight = productDto.ProductWeight,
            Width = productDto.ProductWidth,
            Height = productDto.ProductHeight,
            Depth = productDto.ProductDepth
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        foreach (var categoryId in productDto.ProductCategories)
        {
            _context.ProductCategories.Add(new ProductCategory
            {
                ProductId = product.ProductId,
                CategoryId = categoryId
            });
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        if (product == null)
        {
            return NotFound();
        }

        var result = new
        {
            productId = product.ProductId,
            productName = product.Name,
            productWeight = product.Weight,
            productWidth = product.Width,
            productHeight = product.Height,
            productDepth = product.Depth,
            productCategories = product.ProductCategories.Select(pc => pc.Category.Name)
        };

        return Ok(result);
    }
}