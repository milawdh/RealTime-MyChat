using Domain.API;
using Domain.Audited.Models;
using Domain.DataLayer.Repository;
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
        public IUserInfoContext UserInfoContext;

        public AppBaseDbContex(DbContextOptions<MyChatContext> options)
        : base(options)
        {
        }

        public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is ICreationAuditedEntity)
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }

            return base.Add(entity);
        }
        public virtual ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entity is ICreationAuditedEntity)
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }
            return base.AddAsync(entity, cancellationToken);
        }

        public virtual void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is ICreationAuditedEntity))
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }

            Set<TEntity>().AddRange(entities);
        }

        public virtual void AddRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is ICreationAuditedEntity))
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().AddRange(entities);
        }


        public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IModificationAuditedEntity)
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            return Update(entity);
        }

        public virtual void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is IModificationAuditedEntity))
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is IModificationAuditedEntity))
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = UserInfoContext.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedBy)).CurrentValue = UserInfoContext.User;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> RemoveForce<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Remove(entity);
        }

        public virtual ServiceResult<EntityEntry<TEntity>> Reomve<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is not IBaseAuditedEntity)
                return new ServiceResult<EntityEntry<TEntity>>("Given Object does not Inherit Base Entity");

            Entry(entity).Property(nameof(IBaseAuditedEntity.IsDeleted)).CurrentValue = true;

            return new ServiceResult<EntityEntry<TEntity>>(Entry(entity));
        }

        public virtual ServiceResult ReomveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                if (entity is not IBaseAuditedEntity)
                    return new ServiceResult<EntityEntry<TEntity>>("Given Object does not Inherit Base Entity");

                Entry(entity).Property(nameof(IBaseAuditedEntity.IsDeleted)).CurrentValue = true;
            }
            return new ServiceResult();
        }

        public virtual ServiceResult ReomveRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                if (entity is not IBaseAuditedEntity)
                    return new ServiceResult<EntityEntry<TEntity>>("Given Object does not Inherit Base Entity");

                Entry(entity).Property(nameof(IBaseAuditedEntity.IsDeleted)).CurrentValue = true;
            }
            return new ServiceResult();
        }

        public virtual void RemoveRangeForce<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public virtual void RemoveRangeForce<TEntity>(params TEntity[] entities) where TEntity : class
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
