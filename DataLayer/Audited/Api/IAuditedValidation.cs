using Domain.API;
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
    public interface IAuditedValidation
    {
        /// <summary>
        /// Will Invoke on entity adding to DataBase
        /// </summary>
        /// <param name="entity">Entity you will validate</param>
        /// <returns>ValidationResul</returns>
        ServiceResult ValidateAdd(object entity, Core core);
        /// <summary>
        /// Will Invoke on entity Updating on DataBase
        /// </summary>
        /// <param name="entity">Entity you will validate</param>
        /// <returns>ValidationResul</returns>
        ServiceResult ValidateUpdate(object entity, Core core);
    }
}
