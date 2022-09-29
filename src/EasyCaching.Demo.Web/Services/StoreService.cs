using EasyCaching.Core;
using System;

namespace EasyCaching.Demo.Interceptors.Services
{
    public class StoreService : IStoreService
    {
        private readonly Lazy<IEasyCachingProvider> _memoryProvider;
        private readonly Lazy<IEasyCachingProvider> _redisProvider;

        public StoreService(IEasyCachingProviderFactory factory)
        {
            _memoryProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("memory"));
            _redisProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("redis"));
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

        public Category GetCachedCategory(int id)
        {
            var valueInCache = _redisProvider.Value.Get<Category>($"category:{id}");

            return valueInCache?.Value;
        }

        public Product GetProduct(int id)
        {
            return new Product(id);
        }

        public Product GetCachedProduct(int id)
        {
            var valueInCache = _memoryProvider.Value.Get<Product>($"product:{id}");

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
