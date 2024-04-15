using Domain.API;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Audited.Api
{
    /// <summary>
    /// Each Entity Will Have Own Validator That it can be called or Not
    /// </summary>
    public abstract class AuditedValidation<TEntity>
    {

        /// <summary>
        /// Will be Invoke on entity adding to DataBase
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public virtual ServiceResult ValidateAdd(TEntity entity, Core core)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// Will be Invoke on entity Updating on DataBase
        /// if you don't override it gives You all Entities Without any changes
        /// </summary>
        /// <param name="entities">DbSet's In IQueryable That Will Be Filtered</param>
        /// <returns>A Validated and Filtered Entities Query</returns>
        public virtual IQueryable<TEntity> ValidateGetPermission(Core core, IQueryable<TEntity> entities, IUserInfoContext userInfoContext)
        {
            return entities;
        }

        /// <summary>
        /// Will be Invoked on entity Removing
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public virtual ServiceResult ValidateRemove(TEntity entity, Core core)
        {
            return new ServiceResult();
        }

        /// <summary>
        /// if you don't override it validate will be success at all
        /// </summary>
        /// <param name="entity">Entity Thath Will be Validated</param>
        /// <returns>ValidationResult</returns>
        public virtual ServiceResult ValidateUpdate(TEntity entity, Core core)
        {
            return new ServiceResult();
        }
    }
}
