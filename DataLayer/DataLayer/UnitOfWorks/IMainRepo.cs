using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Services.Repositories
{
    public interface IMainRepo<TEntity> where TEntity : class
    {
        Task<ServiceResult<EntityEntry<TEntity>>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        ServiceResult<EntityEntry<TEntity>> Add(TEntity entity);
        ServiceResult AddRange(IEnumerable<TEntity> entities);
        ServiceResult AddRange(params TEntity[] entities);
        ServiceResult<EntityEntry<TEntity>> Update(TEntity entity);
        ServiceResult UpdateRange(IEnumerable<TEntity> entities);
        ServiceResult UpdateRange(params TEntity[] entities);

        Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> defualtIncludeAsync = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null,
            bool igonoreGlobalQuery = false,
            bool hasSplitQuery = false);

        bool Any(Expression<Func<TEntity, bool>> where = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null);
        TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null);

        Task<TEntity> FirstOrDefualtAsync(Expression<Func<TEntity, bool>> where = null,
                Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> defualtInclude = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            bool igonoreGlobalQuery = true,
            bool hasSplitQuery = true,
            bool hasEntityFilters = true);


        ServiceResult<EntityEntry<TEntity>> Reomve(TEntity entity);
        ServiceResult RemoveRange(params TEntity[] entities);
        ServiceResult RemoveRange(IEnumerable<TEntity> entities);

        ServiceResult<EntityEntry<TEntity>> ReomveForce(TEntity entity);
        ServiceResult RemoveRangeForce(IEnumerable<TEntity> entities);
        ServiceResult RemoveRangeForce(params TEntity[] entities);
    }
}
