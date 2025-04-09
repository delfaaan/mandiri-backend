using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductsService.Models
{
	public class Product
	{
		[Key]
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required, MaxLength(100)]
		[JsonPropertyName("name")]
		public required string Name { get; set; }

		[Required]
		[JsonPropertyName("price")]
		public int Price { get; set; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}