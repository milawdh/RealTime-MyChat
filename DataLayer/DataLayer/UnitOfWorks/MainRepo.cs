using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.API;
using Domain.Audited.Api;
using Domain.Audited.Models;
using Domain.DataLayer.Contexts;
using Domain.DataLayer.Contexts.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Services.Repositories
{
    public class MainRepo<TEntity> : IMainRepo<TEntity> where TEntity : Entity<TEntity>
    {
        private readonly AppBaseDbContex _context;
        private readonly IQueryable<TEntity> Query;
        private readonly TEntity _entityType;

        public MainRepo(AppBaseDbContex context)
        {
            _context = context;
            _entityType = Activator.CreateInstance<TEntity>();
            Query = _entityType.ValidateGetPermission(new Domain.DataLayer.UnitOfWorks.Core(_context), _context.Set<TEntity>().AsQueryable(), context.User);
        }

        #region Add

        public virtual ServiceResult<EntityEntry<TEntity>> Add(TEntity entity)
        {
            if (entity is AuditedValidation<TEntity>)
            {
                var validationResult = entity.ValidateAdd(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                if (validationResult.Failure)
                    return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
            }

            return new ServiceResult<EntityEntry<TEntity>>(_context.Add(entity));
        }

        public virtual async Task<ServiceResult<EntityEntry<TEntity>>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is AuditedValidation<TEntity>)
            {
                var validationResult = entity.ValidateAdd(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                if (validationResult.Failure)
                    return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
            }
            return new ServiceResult<EntityEntry<TEntity>>(await _context.AddAsync(entity));
        }

        public virtual ServiceResult AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateAdd(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }
            _context.AddRange(entities);
            return new ServiceResult();
        }

        public virtual ServiceResult AddRange(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateAdd(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }
            _context.AddRange(entities.ToArray());
            return new ServiceResult();
        }

        #endregion

        #region Update

        public virtual ServiceResult<EntityEntry<TEntity>> Update(TEntity entity)
        {
            if (entity is AuditedValidation<TEntity>)
            {
                var validationResult = entity.ValidateUpdate(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                if (validationResult.Failure)
                    return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
            }
            return new ServiceResult<EntityEntry<TEntity>>(_context.Update(entity));
        }

        public virtual ServiceResult UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateUpdate(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }

            _context.UpdateRange(entities);
            return new ServiceResult();
        }

        public virtual ServiceResult UpdateRange(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateUpdate(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }
            _context.UpdateRange(entities);
            return new ServiceResult();
        }

        #endregion

        #region Soft Remove

        public virtual ServiceResult<EntityEntry<TEntity>> Reomve(TEntity entity)
        {
            if (entity is AuditedValidation<TEntity>)
            {
                var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                if (validationResult.Failure)
                    return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
            }

            return new ServiceResult<EntityEntry<TEntity>>(_context.Remove(entity));
        }

        public virtual ServiceResult RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }
            _context.RemoveRange(entities);
            return new ServiceResult();
        }

        public virtual ServiceResult RemoveRange(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult(validationResult.Messages);
                }
            }
            _context.RemoveRange(entities);
            return new ServiceResult();
        }

        #endregion

        #region Force Remove

        public virtual ServiceResult<EntityEntry<TEntity>> ReomveForce(TEntity entity)
        {
            if (entity is AuditedValidation<TEntity>)
            {
                var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                if (validationResult.Failure)
                    return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
            }

            return new ServiceResult<EntityEntry<TEntity>>(_context.RemoveForce(entity));
        }

        public virtual ServiceResult RemoveRangeForce(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
                }
            }
            _context.RemoveRangeForce(entities);
            return new ServiceResult();
        }

        public virtual ServiceResult RemoveRangeForce(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity is AuditedValidation<TEntity>)
                {
                    var validationResult = entity.ValidateRemove(entity, new Domain.DataLayer.UnitOfWorks.Core(_context));
                    if (validationResult.Failure)
                        return new ServiceResult<EntityEntry<TEntity>>(validationResult.Messages);
                }
            }
            _context.RemoveRangeForce(entities);
            return new ServiceResult();
        }

        #endregion

        #region Get

        public virtual async Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> defualtIncludeAsync = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null,
            bool igonoreGlobalQuery = false,
            bool hasSplitQuery = false)
        {
            IQueryable<TEntity> query = Query.AsQueryable();
            if (igonoreGlobalQuery)
                query = Query.IgnoreQueryFilters();

            if (where != null)
                query = query.Where(where);

            if (defualtIncludeAsync != null)
            {
                if (hasSplitQuery)
                    query = await defualtIncludeAsync(query.AsSplitQuery());
                else
                    query = await defualtIncludeAsync(query.AsSingleQuery());
            }

            if (includesAsync != null)
            {
                if (hasSplitQuery)
                    query = await includesAsync(query.AsSplitQuery());
                else
                    query = await includesAsync(query.AsSingleQuery());
            }

            return query;
        }

        public virtual TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = Query;

            if (includes != null)
                query = includes(query);

            if (where != null)
                return query.FirstOrDefault(where);

            return null;
        }

        public virtual async Task<TEntity> FirstOrDefualtAsync(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null)
        {
            IQueryable<TEntity> query = Query;

            if (includesAsync != null)
                query = await includesAsync(query);

            if (where != null)
                return await query.FirstOrDefaultAsync(where);

            return null;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> defualtInclude = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null,
            bool igonoreGlobalQuery = true,
            bool hasSplitQuery = true)
        {
            IQueryable<TEntity> query = Query;
            if (where != null)
                query = query.Where(where);

            if (defualtInclude != null)
                query = defualtInclude(query);

            if (includes != null)
                query = includes(query);

            if (defualtInclude != null)
            {
                if (hasSplitQuery)
                    query = defualtInclude(query.AsSplitQuery());
                else
                    query = defualtInclude(query.AsSingleQuery());
            }

            if (includes != null)
            {
                if (hasSplitQuery)
                    query = includes(query.AsSplitQuery());
                else
                    query = includes(query.AsSingleQuery());
            }

            return query;
        }

        #endregion

        #region Predicate

        public virtual bool Any(Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
                return Query.Any(where);
            return false;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
                return await Query.AnyAsync(where);
            return false;
        }

        #endregion
    }
}
