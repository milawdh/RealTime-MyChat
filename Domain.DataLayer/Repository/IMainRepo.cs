using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Services.Repositories
{
    public interface IMainRepo<TEntity> where TEntity : class
    {
        EntityEntry<TEntity> Add(TEntity entity);
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void AddRange(IEnumerable<TEntity> entities);
        void AddRange(params TEntity[] entities);
        EntityEntry<TEntity> Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void UpdateRange(params TEntity[] entities);
        EntityEntry<TEntity> Delete(TEntity entity);
        EntityEntry<TEntity> DeleteById(object id);
        Task<EntityEntry<TEntity>> DeleteByIdAsync(object id);
        Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> defualtIncludeAsync = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null,
            bool igonoreGlobalQuery = false,
            bool hasSplitQuery = false);

        bool Any(Expression<Func<TEntity, bool>> where = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null);

        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);

        TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> FirstOrDefualtAsync(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> defualtInclude = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);
    }
}
