using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOfRepositorie.Models
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
		{

		}

		public DbSet<Category> demo_categories { get; set; }
		public DbSet<CoverType> demo_covertypes { get; set; }
		public DbSet<Product> demo_products { get; set; }
		public DbSet<ApplicationUser> application_users { get; set; }
		public DbSet<Company> companies { get; set; }
		public DbSet<ShoppingCart> shoppingCarts { get; set; }
	}
}
