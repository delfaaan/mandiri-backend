using Microsoft.EntityFrameworkCore;
using OrdersService.Models;

namespace OrdersService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Order> Orders => Set<Order>();
	}
}