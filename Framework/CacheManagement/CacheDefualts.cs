using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CacheManagement
{
    public static class CacheDefualts
    {
        public static int DefaultAbsolouteExpireMinutes = 31;
        public static int DefaultSizeLimit = 1024;
        public static int DefaultSize = 1;
        public static int DefaultSlidingExpirationMinutes = 5;
        public static double DefaultCompactPercentage = 0.5;
        public static CacheItemPriority DefaultPriority = CacheItemPriority.High;
    }
}
