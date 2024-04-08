using Domain.API;
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
    /// <typeparam name="TEntity">Entity Model That Interface Implementing Now</typeparam>
    public interface IAuditedValidation<TEntity>
    {
        ServiceResult ValidateAdd(TEntity entity);
        ServiceResult ValidateUpdate(TEntity entity);
    }
}
