using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;

namespace ApplicationOfRepositorie.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private ApplicationDbContext _db;
		public OrderHeaderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
 
		public void Update(OrderHeader obj)
		{
			_db.orderHeaders.Update(obj);
		}

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.orderHeaders.FirstOrDefault(u=>u.Id == id);
			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if(paymentStatus != null)
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}
	}
}
