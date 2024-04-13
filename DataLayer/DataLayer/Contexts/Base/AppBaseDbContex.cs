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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Contexts.Base
{
    public class AppBaseDbContex : DbContext
    {
        public UserInfoContext User { get; set; }
        public AppBaseDbContex(DbContextOptions<AppBaseDbContex> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.GetAssembly(typeof(TblChatRoom));
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            LoadEntities(modelBuilder, "Domain.Entities");

        }

        protected void LoadEntities(ModelBuilder modelBuilder, string nameSpace)
        {
            var model = modelBuilder.Model;

            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });

            var entityTypes = model.GetEntityTypes()
                .Where(x => x.ClrType.Namespace.StartsWith(nameSpace)).Where(x => x.ClrType.IsSubclassOf(typeof(Entity<>)))
                .Select(x => x.ClrType).ToList();

            entityTypes.ForEach(t =>
            {
                entityMethod.MakeGenericMethod(t).Invoke(modelBuilder, new object[] { });
            });
        }

        public virtual EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {

            Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = User.UserId;
            Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;

            return Set<TEntity>().Add(entity);
        }
        public virtual async Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            if (entity is ICreationAuditedEntity)
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }
            return await base.AddAsync(entity, cancellationToken);
        }
        public virtual TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return Set<TEntity>().Find(id);
        }
        public virtual void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is ICreationAuditedEntity))
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }

            Set<TEntity>().AddRange(entities);
        }
        public virtual bool Any<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            return Set<TEntity>().Any(predicate);
        }
        public virtual void AddRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is ICreationAuditedEntity))
            {
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(ICreationAuditedEntity.CreatedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().AddRange(entities);
        }


        public virtual EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IModificationAuditedEntity)
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            return Set<TEntity>().Update(entity);
        }

        public virtual void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is IModificationAuditedEntity))
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual void UpdateRange<TEntity>(params TEntity[] entities) where TEntity : class
        {
            foreach (var entity in entities.Where(x => x is IModificationAuditedEntity))
            {
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(IModificationAuditedEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }
            Set<TEntity>().UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> RemoveForce<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Remove(entity);
        }

        public virtual void RemoveRangeForce<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public virtual void RemoveRangeForce<TEntity>(params TEntity[] entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public virtual ServiceResult<EntityEntry<TEntity>> Reomve<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is not IBaseAuditedEntity)
                return new ServiceResult<EntityEntry<TEntity>>("Given Object does not Inherit Base Entity");

            Entry(entity).Property(nameof(IBaseAuditedEntity.IsDeleted)).CurrentValue = true;

            if (entity is IDeleteAuditedEntity)
            {
                Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteById)).CurrentValue = User.UserId;
                Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteDate)).CurrentValue = DateTime.Now;
            }


            return new ServiceResult<EntityEntry<TEntity>>(Entry(entity));
        }

        public virtual ServiceResult ReomveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (var entity in entities)
            {
                if (entity is not IBaseAuditedEntity)
                    return new ServiceResult<EntityEntry<TEntity>>("Given Object does not Inherit Base Entity");

                Entry(entity).Property(nameof(IBaseAuditedEntity.IsDeleted)).CurrentValue = true;

                if (entity is IDeleteAuditedEntity)
                {
                    Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteById)).CurrentValue = User.UserId;
                    Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteDate)).CurrentValue = DateTime.Now;
                }
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

                if (entity is IDeleteAuditedEntity)
                {
                    Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteById)).CurrentValue = User.UserId;
                    Entry(entity).Property(nameof(IDeleteAuditedEntity.DeleteDate)).CurrentValue = DateTime.Now;
                }
            }
            return new ServiceResult();
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
