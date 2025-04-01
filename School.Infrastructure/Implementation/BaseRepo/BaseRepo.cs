using School.Application.Interfaces.BaseRepo;
using School.Infrastructure.Data;
using System.Linq.Expressions;

namespace School.Infrastructure.Implementation.BaseRepo
{
	public class BaseRepo<T>(ApplicationDbContext dbContext) : IBaseRepo<T> where T : class
	{
		private readonly ApplicationDbContext _dbContext = dbContext;
		public async Task<T> GetById(int id, CancellationToken cancellationToken = default) =>
			await _dbContext.Set<T>().FindAsync(id);
		public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
			await _dbContext.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
		public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
			await _dbContext.Set<T>().FindAsync(id, cancellationToken);
		public async Task<T> CreateAsync(T Entity, CancellationToken cancellationToken = default)
		{
			await _dbContext.Set<T>().AddAsync(Entity, cancellationToken = default);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return Entity;
		}
		public async Task<T> UpdateAsync(T Entity, CancellationToken cancellationToken = default)
		{
			_dbContext.Set<T>().Update(Entity);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return Entity;
		}

		public async Task<T> DeleteAsync(T Entity, CancellationToken cancellationToken = default)
		{
			var res = _dbContext.Set<T>().Remove(Entity);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return Entity;
		}
		public async Task<T> FindByInclude(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default, string[] Include = null)
		{
			IQueryable<T> obj = _dbContext.Set<T>();
			if (Include != null)
			{
				foreach (var item in Include)
				{
					obj = obj.Include(item);
				}
			}
			return await obj.FirstOrDefaultAsync(match, cancellationToken);
		}
		public async Task<IQueryable<T>> FindAllInclude(CancellationToken cancellationToken = default, string[] Include = null)
		{
			IQueryable<T> obj = _dbContext.Set<T>();
			if (Include != null)
			{
				foreach (var item in Include)
				{
					obj = obj.Include(item);
				}
			}
			return obj;
		}
		public async Task<IQueryable<T>> FindAllByInclude(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default, string[] Include = null)
		{
			IQueryable<T> obj = _dbContext.Set<T>();
			if (Include != null)
			{
				foreach (var item in Include)
				{
					obj = obj.Include(item);
				}
			}
			return obj.Where(match);
		}
	}
}
