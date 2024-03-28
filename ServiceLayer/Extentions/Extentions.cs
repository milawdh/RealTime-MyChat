using Domain.Models;
using DomainShared.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Extentions
{
    public static class Extentions
    {

        /// <summary>
        /// Hashes string With SHA256
        /// </summary>
        /// <param name="source">String that will hash</param>
        /// <returns></returns>
        public static string HashData(this string source)
        {
            SHA256 hasher = SHA256.Create();
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            var Result = "";
            foreach (byte b in bytes)
            {
                Result += b.ToString("x2");
            }
            return Result;
        }
        
        public static ServiceResult<object> ToObjectiveServiceResult<T>(this ServiceResult<T> serviceResult)
        {
            if (serviceResult.Failure)
                return new ServiceResult<object>(serviceResult.Messages);

            var result = new ServiceResult<object>(serviceResult.Result);

            result.Messages = new();
            result.Messages.AddRange(serviceResult.Messages);
            return result;
        }
    
    
    }
}
