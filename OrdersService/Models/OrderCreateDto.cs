using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrdersService.Models
{
	public class OrderCreateDto
	{
		[Required]
		[JsonPropertyName("user_id")]
		public Guid UserId { get; set; }

		[Required]
		[JsonPropertyName("product_id")]
		public Guid ProductId { get; set; }

		[Required]
		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }
	}
}