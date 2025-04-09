using Microsoft.EntityFrameworkCore;
using ProductsService.Models;

namespace ProductsService.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Product> Products => Set<Product>();
	}
}