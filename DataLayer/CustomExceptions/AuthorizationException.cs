using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CustomExceptions
{
    public class AuthorizationException : Exception
    {
        /// <summary>
        /// Occurs In Authenticate and Athorize Invalidation
        /// </summary>
        /// <param name="message"></param>
        public AuthorizationException(string message) : base(message) { }
    }
}
