using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace OrdersService.Models
{
	[Index(nameof(UserId))]
	[Index(nameof(ProductId))]
	public class Order
	{
		[Key]
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required]
		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		[Required]
		[JsonPropertyName("product_id")]
		public Guid ProductId { get; set; }

		[Required]
		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }

		[JsonPropertyName("total")]
		public int Total { get; set; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}