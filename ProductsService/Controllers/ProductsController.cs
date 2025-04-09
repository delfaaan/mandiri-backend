using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Models;

namespace ProductsService.Controller
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProductsController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			return await _context.Products.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(Guid id)
		{
			var product = await _context.Products.FindAsync(id);

			return product == null ? NotFound() : Ok(product);
		}

		[HttpPost]
		public async Task<ActionResult<Product>> CreateProduct(Product product)
		{
			_context.Products.Add(product);

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(Guid id, Product updatedProduct)
		{
			if (id != updatedProduct.Id) return BadRequest();

			_context.Entry(updatedProduct).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			var product = await _context.Products.FindAsync(id);

			if (product == null) return NotFound();

			_context.Products.Remove(product);

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}