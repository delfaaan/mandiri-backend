using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Data;
using OrdersService.Models;
using System.Text.Json;

namespace OrdersService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _config;

		public OrdersController(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration config)
		{
			_context = context;
			_httpClientFactory = httpClientFactory;
			_config = config;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderCreateDto dto)
		{
			var client = _httpClientFactory.CreateClient();

			var userApi = _config["Services:Users"];
			var userRes = await client.GetAsync($"{userApi}/api/users/{dto.UserId}");

			if (!userRes.IsSuccessStatusCode) return BadRequest(new { message = "User not found " });

			var userJson = await userRes.Content.ReadAsStringAsync();
			var user = JsonSerializer.Deserialize<UserDto>(userJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (user == null) return BadRequest(new { message = "Failed to parse user" });

			var productApi = _config["Services:Products"];
			var productRes = await client.GetAsync($"{productApi}/api/products/{dto.ProductId}");

			if (!productRes.IsSuccessStatusCode) return BadRequest(new { message = "Product not found " });

			var productJson = await productRes.Content.ReadAsStringAsync();
			var product = JsonSerializer.Deserialize<ProductDto>(productJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (product == null) return BadRequest(new { message = "Failed to parse product" });

			var order = new Order
			{
				UserId = dto.UserId,
				ProductId = dto.ProductId,
				Quantity = dto.Quantity,
				Total = product.Price * dto.Quantity
			};

			_context.Orders.Add(order);

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrder(Guid id)
		{
			var order = await _context.Orders.FindAsync(id);

			return order == null ? NotFound() : Ok(order);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(Guid id)
		{
			var order = await _context.Orders.FindAsync(id);

			if (order == null) return NotFound();

			_context.Orders.Remove(order);

			await _context.SaveChangesAsync();

			return NoContent();
		}

		private class ProductDto
		{
			public Guid Id { get; set; }
			public required string Name { get; set; }
			public int Price { get; set; }
		}
	}
}