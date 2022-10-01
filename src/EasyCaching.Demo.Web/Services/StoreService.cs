using EasyCaching.Core;
using EasyCaching.Demo.Web.Services.Entities;
using System;

namespace EasyCaching.Demo.Interceptors.Services
{
    public class StoreService : IStoreService
    {
        private readonly Lazy<IEasyCachingProvider> _easyMemoryCachingProvider;
        private readonly Lazy<IEasyCachingProvider> _easyRedisCachingProvider;
        private readonly Lazy<IRedisCachingProvider> _redisCachingProvider;

        public StoreService(IEasyCachingProviderFactory factory)
        {
            _easyMemoryCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("memory"));
            _easyRedisCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("redis"));
            _redisCachingProvider = new Lazy<IRedisCachingProvider>(factory.GetRedisProvider("redis"));
        }

        public void DeleteCategory(int id)
        {
        }

        public void DeleteProduct(int id)
        {
        }

        public Category GetCategory(int id)
        {
            return new Category(id);
        }

        public string GetRawCacheCategory(int id)
        {
            var cacheKey = $"category:{id}";

            var redisValue = _redisCachingProvider.Value.StringGet(cacheKey);

            return redisValue;
        }

        public Category GetCachedCategory(int id)
        {
            var valueInCache = _easyRedisCachingProvider.Value.Get<Category>($"category:{id}");

            return valueInCache?.Value;
        }

        public Product GetProduct(int id)
        {
            return new Product(id);
        }

        public Product GetCachedProduct(int id)
        {
            var cacheKey = $"product:{id}";

            var valueInCache = _easyMemoryCachingProvider.Value.Get<Product>(cacheKey);

            return valueInCache?.Value;
        }

        public Category PutCategory(Category category)
        {
            return category;
        }

        public Product PutProduct(Product product)
        {
            return product;
        }
    }
}
