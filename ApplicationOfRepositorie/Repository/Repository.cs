using ApplicationOfRepositorie.Models;
using ApplicationOfRepositorie.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationOfRepositorie.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private ApplicationDbContext _db;
		internal DbSet<T> dbSet;

		public Repository(ApplicationDbContext db)
		{
			_db= db;
			//_db.demo_products.Include(i => i.Category).Include(u => u.CoverType);
			this.dbSet = _db.Set<T>();
		}
		public void Add(T entity)
		{
			_db.Add(entity);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			if(includeProperties != null)
			{
				foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(property);
				}
			}
			return query.ToList();
		}

		public T GetFirstorDefault(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

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
