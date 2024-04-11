using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.DataLayer.Contexts;
using Domain.DataLayer.Contexts.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Services.Repositories
{
    public class MainRepo<TEntity> : IMainRepo<TEntity> where TEntity : class
    {
        private readonly AppBaseDbContex _context;
        private readonly DbSet<TEntity> _dbSet;

        public MainRepo(AppBaseDbContex context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            return _context.Add(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return await _context.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
        }

        public virtual void AddRange(params TEntity[] entities)
        {
            _context.AddRange(entities.ToArray());
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            dynamic model = entity;
            _dbSet.Local.FindEntry(model.Id).State = EntityState.Detached;
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _context.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);
        }

        public virtual void UpdateRange(params TEntity[] entities)
        {
            _context.UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);

                return _context.Remove(entity);
            }
            catch
            {
                return _context.Entry(entity);
            }
        }

        public virtual EntityEntry<TEntity> DeleteById(object id)
        {

            TEntity entity = _dbSet.Find(id);
            return Delete(entity);
        }

        public virtual async Task<EntityEntry<TEntity>> DeleteByIdAsync(object id)
        {
            TEntity entity = _dbSet.Find(id);
            return Delete(entity);

        }

        public virtual async Task<IQueryable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> defualtIncludeAsync = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null,
            bool igonoreGlobalQuery = false,
            bool hasSplitQuery = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (igonoreGlobalQuery)
                query = _dbSet.IgnoreQueryFilters();


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

        public virtual bool Any(Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
                return _dbSet.Any(where);
            return false;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where = null)
        {
            if (where != null)
                return await _dbSet.AnyAsync(where);
            return false;
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual TEntity FirstOrDefualt(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
                query = includes(query);

            if (where != null)
                return query.FirstOrDefault(where);

            return null;
        }

        public virtual async Task<TEntity> FirstOrDefualtAsync(Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> includesAsync = null)
        {
            IQueryable<TEntity> query = _dbSet;

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
            IQueryable<TEntity> query = _dbSet;
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
    }
}
