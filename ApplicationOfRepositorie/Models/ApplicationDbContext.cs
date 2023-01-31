using Microsoft.EntityFrameworkCore;

namespace ApplicationOfRepositorie.Models
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
		{

		}

		public DbSet<Category> demo_categories { get; set; }
		public DbSet<CoverType> demo_covertypes { get; set; }
		public DbSet<Product> demo_products { get; set; }
	}
}
