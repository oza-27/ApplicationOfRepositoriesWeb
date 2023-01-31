using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;

namespace ApplicationOfRepositorie.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(Product obj)
		{
			_db.demo_products.Update(obj);
		}
	}
}
