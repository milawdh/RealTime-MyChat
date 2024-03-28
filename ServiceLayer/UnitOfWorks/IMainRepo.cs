using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Services.Repositories
{
    public interface IMainRepo<T> where T : class
    {
        EntityEntry Add(T entity);

        Task<EntityEntry> AddAsync(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        bool DeleteById(object id);

        Task<bool> DeleteByIdAsync(object id);

        T GetById(object id);

        IQueryable<T> Get(Expression<Func<T, bool>> where = null,
			Func<IQueryable<T>, IQueryable<T>> defualtInclude = null, Func<IQueryable<T>, IQueryable<T>> includes = null);

        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includes);

        bool Any(Expression<Func<T, bool>> where = null);

        Task<bool> AnyAsync(Expression<Func<T, bool>> where = null);

        Task<T> GetByIdAsync(object id);

        T SingleOrDefault(Expression<Func<T, bool>> where = null);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where = null);


    }
}
