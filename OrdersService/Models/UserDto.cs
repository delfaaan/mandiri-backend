namespace OrdersService.Models
{
	public class UserDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public required string Email { get; set; }
	}
}