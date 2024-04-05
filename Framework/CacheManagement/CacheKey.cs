using DomainShared.Services;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.CacheManagement
{
    public class CacheKey
    {
        public string Key { get; set; }

        public int? AbsouluteExpireMinutes { get; set; }
        public int? SlidingExpireMinutes { get; set; }
        public CacheItemPriority? Priority { get; set; }
        public int? Size { get; set; }

        public MemoryCacheEntryOptions EntryOptions
        {
            get
            {
                var res = new MemoryCacheEntryOptions();

                if (AbsouluteExpireMinutes != null)
                    res.SetAbsoluteExpiration(TimeSpan.FromMinutes(AbsouluteExpireMinutes.Value));
                else
                    res.SetAbsoluteExpiration(TimeSpan.FromMinutes(CacheDefualts.DefaultAbsolouteExpireMinutes));

                if (SlidingExpireMinutes != null)
                    res.SetSlidingExpiration(TimeSpan.FromMinutes(SlidingExpireMinutes.Value));
                else
                    res.SetSlidingExpiration(TimeSpan.FromMinutes(CacheDefualts.DefaultSlidingExpirationMinutes));

                if (Priority != null)
                    res.SetPriority(Priority.Value);
                else
                    res.SetPriority(CacheDefualts.DefaultPriority);

                if (Size != null)
                    res.SetSize(Size.Value);
                else
                    res.SetSize(CacheDefualts.DefaultSize);

                return res;
            }
        }

        public CacheKey(string key,
            int? absouluteExpireMinutes = null,
            int? slidingExpireMinutes = null,
            CacheItemPriority? priority = null,
            int? size = null)
        {
            Key = key;
            AbsouluteExpireMinutes = absouluteExpireMinutes;
            SlidingExpireMinutes = slidingExpireMinutes;
            Priority = priority;
            Size = size;
        }

    }
}
