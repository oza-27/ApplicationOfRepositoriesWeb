using ApplicationOfRepositorie.Models;

namespace ApplicationOfRepositorie.Repository.IRepository
{
	public interface IOrderDetailRepository : IRepository<OrderDetail>
	{
		void Update(OrderDetail obj);
	}
}
