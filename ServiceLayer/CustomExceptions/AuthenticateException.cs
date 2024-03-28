using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomExceptions
{
    public class AuthenticateException : Exception
    {
        /// <summary>
        /// Occurs In Authenticate and Athorize Invalidation
        /// </summary>
        /// <param name="message"></param>
        public AuthenticateException(string message) : base(message) { }
    }
}
