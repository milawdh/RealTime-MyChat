using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Profiles
{
    public static class CacheKeysProfile
    {
        public static class UserInfo
        {
            /// <summary>
            /// Has Argument 0 : UserId
            /// </summary>
            public static string Prefix = "User_{0}_";

            /// <summary>
            /// Has Argument 0 : UserId
            /// </summary>
            public static string Contacts = Prefix + "Contacts";
        }
    }
}
