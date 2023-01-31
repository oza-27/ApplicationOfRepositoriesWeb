using ApplicationOfRepositorie.Models;

namespace ApplicationOfRepositorie.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product obj);
	}
}
