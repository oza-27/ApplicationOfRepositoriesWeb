using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;

namespace ApplicationOfRepositorie.Repository
{
	public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
		private ApplicationDbContext _db;
		public OrderDetailRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(OrderDetail obj)
		{
			_db.orderDetail.Update(obj);
		}
	}
}
