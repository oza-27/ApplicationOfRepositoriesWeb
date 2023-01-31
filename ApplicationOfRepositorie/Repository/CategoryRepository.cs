using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;

namespace ApplicationOfRepositorie.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(Category obj)
		{
			_db.demo_categories.Update(obj);
		}
	}
}
