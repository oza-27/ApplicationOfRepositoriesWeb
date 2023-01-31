using System.Linq.Expressions;

namespace ApplicationOfRepositorie.Repository.IRepository
{
	public interface IRepository<T> where T: class
	{
		// T- category namw
		T GetFirstofDefault(Expression<Func<T, bool>> filter);
		IEnumerable<T> GetAll();
		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entity);
	}
}
