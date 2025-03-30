using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Interfaces.BaseRepo
{
    public interface IBaseRepo<T> where T:class
    {
         Task<T> GetById(int id, CancellationToken cancellationToken = default);
		 Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
		 Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<T> FindByInclude(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default, string[] Include = null);
		Task<IQueryable<T>> FindAllInclude(CancellationToken cancellationToken = default, string[] Include = null);
		 Task<T> CreateAsync(T Entity, CancellationToken cancellationToken = default);
		 Task<T> UpdateAsync(T Entity, CancellationToken cancellationToken = default);
		 Task<T> DeleteAsync(T Entity, CancellationToken cancellationToken = default);
		Task<IQueryable<T>> FindAllByInclude(Expression<Func<T, bool>> match, CancellationToken cancellationToken = default, string[] Include = null);
	}
}
