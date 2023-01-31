using ApplicationOfRepositorie.Models;

namespace ApplicationOfRepositorie.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category obj);
	}
}
