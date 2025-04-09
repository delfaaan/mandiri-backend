using Microsoft.EntityFrameworkCore;
using UsersService.Models;

namespace UsersService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<User> Users => Set<User>();
	}
}