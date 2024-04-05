using Domain.API;
using Framework.CacheManagement;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// Adds Cache Item With Given Key
        /// </summary>
        /// <param name="cacheKey">Cache Item Key</param>
        /// <param name="value">Cache Item Value</param>
        void Set(CacheKey cacheKey, object value);

        /// <summary>
        /// Gets A Cache Item With Given Key and if it couldn't find that key will call aquire Method
        /// </summary>
        /// <typeparam name="TOut">aquire Method OutPut</typeparam>
        /// <param name="key">Cached Item's Key</param>
        /// <param name="aquire">Method that will return a value if cache couldn't been found!</param>
        /// <returns></returns>
        Task<ServiceResult<TOut>> GetAsync<TOut>(CacheKey key, Func<TOut> aquire);

        /// <summary>
        /// Removes Cache item wich has key equals exactly with given key 
        /// </summary>
        /// <param name="key">Cache Item exact Key</param>
        void Remove(string key);

        /// <summary>
        /// Removes CacheItems that their keys starts with this prefixes
        /// </summary>
        /// <param name="prefixes">prefixes that those will delete keys should starts with them</param>
        void RemoveByPrefix(params string[] prefixes);
    }

    public class CacheManager : ICacheManager
    {
        #region Constructor

        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// All Cache Keys that exist in memory
        /// </summary>
        private static List<string> _keys;
        public CacheManager(IMemoryCache memoryCache)
        {
            _keys = _keys ?? new();
            _memoryCache = memoryCache;
        }

        #endregion

        #region Set Value

        /// <summary>
        /// Sets Cache item but does every option and setting before set
        /// </summary>
        /// <param name="key">Cache Item exact Key</param>
        /// <param name="entryOptions">Cache Item Options</param>
        /// <param name="value">Cache Item Value</param>
        private void Set(string key, MemoryCacheEntryOptions entryOptions, object value)
        {
            if (!_keys.Contains(key))
            {
                entryOptions
                    .RegisterPostEvictionCallback(PostEvictionCallback, _memoryCache);

                _keys.Add(key);
                _memoryCache.Set(key, value, entryOptions);
            }
        }

        /// <summary>
        /// Adds Cache Item With Given Key
        /// </summary>
        /// <param name="cacheKey">Cache Item Key</param>
        /// <param name="value">Cache Item Value</param>
        public void Set(CacheKey cacheKey, object value)
        {
            Set(cacheKey.Key, cacheKey.EntryOptions, value);
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets A Cache Item With Given Key and if it couldn't find that key will call aquire Method
        /// </summary>
        /// <typeparam name="TOut">aquire Method OutPut</typeparam>
        /// <param name="key">Cached Item's Key</param>
        /// <param name="aquire">Method that will return a value if cache couldn't been found!</param>
        /// <returns></returns>
        public async Task<ServiceResult<TOut>> GetAsync<TOut>(CacheKey key, Func<TOut> aquire)
        {
            if (!_memoryCache.TryGetValue(key.Key, out var value))
            {
                value = aquire();

                if (value == null)
                    return new ServiceResult<TOut>("Delegeate Didn't Return a Value");

                Set(key, value);
            }
            return new ServiceResult<TOut>((TOut)value);
        }

        #endregion

        #region Remove Value

        /// <summary>
        /// Removes Cache item wich has key equals exactly with given key 
        /// </summary>
        /// <param name="key">Cache Item exact Key</param>
        public void Remove(string key)
        {
            if (_keys.Contains(key))
            {
                _memoryCache.Remove(key);
            }
        }
        /// <summary>
        /// Removes CacheItems that their keys starts with this prefixes
        /// </summary>
        /// <param name="prefixes">prefixes that those will delete keys should starts with them</param>
        public void RemoveByPrefix(params string[] prefixes)
        {
            var willDeleteKeys = _keys.Where(key => prefixes.Any(prefix => key.StartsWith(prefix)));

            foreach (string key in willDeleteKeys)
            {
                _memoryCache.Remove(key);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that triggs after a Cache Item been deleted
        /// </summary>
        private void PostEvictionCallback(object cacheKey, object? cacheValue, EvictionReason evictionReason, object? state)
        {
            if (_keys.Contains((string)cacheKey))
            {
                _keys.Remove((string)cacheKey);
            }
        }

        #endregion
    }
}
