using EasyCaching.Core.Interceptor;
using EasyCaching.Demo.Web.Services.Entities;

namespace EasyCaching.Demo.Interceptors.Services
{
    public interface ICachedStoreService
    {
        Product GetCachedProduct(int id);
        Category GetCachedCategory(int id);
        string GetRawCacheCategory(int id);
    }

    public interface IStoreService : ICachedStoreService
    {

        [EasyCachingAble(CacheProviderName = "memory", IsHybridProvider = false, CacheKeyPrefix = "product", Expiration = 10)]
        Product GetProduct(int id);

        [EasyCachingPut(CacheProviderName = "memory", IsHybridProvider = false, CacheKeyPrefix = "product")]
        Product PutProduct(Product product);

        [EasyCachingEvict(CacheProviderName = "memory", IsHybridProvider = false, CacheKeyPrefix = "product", IsBefore = true)]
        void DeleteProduct(int id);


        [EasyCachingAble(CacheProviderName = "redis", IsHybridProvider = false, CacheKeyPrefix = "category", Expiration = 10)]
        Category GetCategory(int id);

        [EasyCachingPut(CacheProviderName = "redis", IsHybridProvider = false, CacheKeyPrefix = "category")]
        Category PutCategory(Category category);

        [EasyCachingEvict(CacheProviderName = "redis", IsHybridProvider = false, CacheKeyPrefix = "category", IsBefore = true)]
        void DeleteCategory(int id);
    }
}
