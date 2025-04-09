using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace UsersService.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public class User
	{
		[Key]
		[JsonPropertyName("id")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Required, MaxLength(100)]
		[JsonPropertyName("name")]
		public required string Name { get; set; }

		[Required, MaxLength(100), EmailAddress]
		[JsonPropertyName("email")]
		public required string Email { get; set; }

		[JsonPropertyName("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}