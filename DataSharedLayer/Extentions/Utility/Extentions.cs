using Domain.API;
using System.Security.Cryptography;
using System.Text;

namespace DomainShared.Extentions.Utility
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

        public static string FormatLength(this string src, int length)
        {
            var isLong = false;
            var res = "";
            char[] srcArray = src.ToArray();

            if (srcArray.Length > length)
            {
                isLong = true;
            }
            else
            {
                length = srcArray.Length;
            }

            for (var i = 0; i < length; i++)
            {
                res += srcArray[i].ToString();
            }

            if (isLong)
            {
                res += " ...";
            }

            return res;
        }
    }
}
