using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApplicationOfRepositorie.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private ApplicationDbContext _db;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext db)
		{
			_db= db;
			this.dbSet = _db.Set<T>();
		}
		public void Add(T entity)
		{
			_db.Add(entity);
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}

		public T GetFirstofDefault(System.Linq.Expressions.Expression<Func<T, bool>> filter)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public void Remove(T entity)
		{
			_db.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			_db.RemoveRange(entity);
		}
	}
}
