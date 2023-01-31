using ApplicationOfRepositorie.Models;

namespace ApplicationOfRepositorie.Repository.IRepository
{
	public interface ICoverTypeRepository : IRepository<CoverType>
	{
		void Update(CoverType obj);
	}
}
