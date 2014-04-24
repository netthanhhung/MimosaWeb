using System;
using System.Web;
using System.Web.Caching;


namespace MVCCore.Framework.Web.Caching
{

    /// <summary>
    /// create generic server cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseCache<T> where T : class
    {
        private string cacheName = string.Empty;
        private Func<T> generateObjectMethod = null;
        private bool supportBackgroundCache = false;
        private int minutesToElapse = 0;
        private string dependencyFilePath = string.Empty;       

        public BaseCache()
        {
        }

        public BaseCache(int minutesToElapse, Func<T> generateObjectMethod, bool supportBackgroundCache, string dependencyFilePath = null, string cacheName = "")
        {
            this.minutesToElapse = minutesToElapse;

            if (!string.IsNullOrEmpty(cacheName))
                this.cacheName = cacheName;
            else
                this.cacheName = string.Format("{0}-{1}", typeof(T).Name, Guid.NewGuid());

            this.generateObjectMethod = generateObjectMethod;
            this.supportBackgroundCache = supportBackgroundCache;
            this.dependencyFilePath = dependencyFilePath;

            Refesh();
        }

        public T Get()
        {
            if (supportBackgroundCache)
                return (T)HttpRuntime.Cache[string.Format("{0}-{1}", "BK", this.cacheName)];

            return (T)HttpRuntime.Cache[this.cacheName];
        }

        private void Refesh()
        {
            T obj = generateObjectMethod(); // heavy load
            if (obj != null)
            {
                Update(obj);
            }
        }

        private void Update(T obj)
        {
            CacheDependency dependencies = null;
            if (!string.IsNullOrEmpty(dependencyFilePath))
                dependencies = new CacheDependency(dependencyFilePath);

            #region 1. cache tồn tại hoài
            if (minutesToElapse <= 0)
            {
                HttpRuntime.Cache.Insert(
                     this.cacheName,
                     obj,
                     dependencies,
                     System.Web.Caching.Cache.NoAbsoluteExpiration,
                     System.Web.Caching.Cache.NoSlidingExpiration,
                      CacheItemPriority.NotRemovable,
                     null);

                return;
            }
            #endregion

            #region 2. cache tồn tại theo khoảng thời gian ấn định

            // đối tượng này chứa dữ liệu thật sự
            if (supportBackgroundCache)
            {
                HttpRuntime.Cache.Insert(
                    string.Format("{0}-{1}", "BK", this.cacheName),
                    obj,
                    null, // luôn luôn phải gán null
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    System.Web.Caching.Cache.NoSlidingExpiration,
                     CacheItemPriority.NotRemovable,
                    null);

                obj = null;
            }

            // đối tượng cache này chỉ dùng cho việc refresh lại cache và obj luôn luôn rỗng
            HttpRuntime.Cache.Insert(
                    this.cacheName,
                    obj ?? new object(),
                    dependencies,
                    DateTime.Now.AddMinutes(minutesToElapse),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.Default,
                    OnCacheRemove
                );
            #endregion
        }

        public void ForceRefesh(T obj)
        {
            Update(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheItem">old value</param>
        /// <param name="reason"></param>
        private void OnCacheRemove(string key, object cacheItem, CacheItemRemovedReason reason)
        {
            try
            {
                if (key == cacheName)//we must reload Cache
                {
                    Refesh();
                    //Logger.DefaultLogger.InfoFormat("OK - OnCacheRemove - key: {0}, {1}", key, dependencyFilePath);
                }
            }
            catch (Exception ex)
            {
                //Logger.DefaultLogger.ErrorFormat("Error - OnCacheRemove - key: {0}, {1}, {2}", key, dependencyFilePath, ex.ToString());

                CacheDependency dependencies = null;
                if (!string.IsNullOrEmpty(dependencyFilePath))
                    dependencies = new CacheDependency(dependencyFilePath);

                HttpRuntime.Cache.Insert(
                 this.cacheName,
                 new object(),
                 dependencies,
                 DateTime.Now.AddMinutes(minutesToElapse),
                 Cache.NoSlidingExpiration,
                 CacheItemPriority.Default,
                 OnCacheRemove
             );
            }
        }


    }
}
