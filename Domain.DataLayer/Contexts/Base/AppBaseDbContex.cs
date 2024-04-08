using Domain.API;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Contexts.Base
{
    public class AppBaseDbContex : DbContext
    {
        public  AppBaseDbContex()
        {

        }

        public AppBaseDbContex(DbContextOptions<MyChatContext> options)
        : base(options)
        {
        }

        public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Add(entity);
        }
        public virtual ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            return base.AddAsync(entity, cancellationToken);
        }

        public virtual void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public virtual void AddRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }


        public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            return Update(entity);
        }

        public virtual void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
        {
            return Remove(entity);
        }

        public virtual void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public virtual void RemoveRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public virtual IDbContextTransaction BeginTransaction()
        {
           return Database.BeginTransaction();
        }
        public virtual void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }
        public virtual void CommitTransaction()
        {
            Database.CommitTransaction();
        }
        public virtual void Dispose()
        {
            base.Dispose();
        }
        public virtual string? GetCurrentConnectionString()
        {
            return Database?.GetConnectionString();
        }

        public virtual ServiceResult SaveChanges()
        {
            if (base.SaveChanges() > 0)
                return new ServiceResult();
            else
                return new ServiceResult("Error occured in saving data");
            
        }
    }
}
