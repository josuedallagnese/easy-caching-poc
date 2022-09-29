using EasyCaching.Core.Interceptor;

namespace EasyCaching.Demo.Interceptors.Services
{
    public interface ICachedStoreService
    {
        Product GetCachedProduct(int id);
        Category GetCachedCategory(int id);
    }

    public interface IStoreService : ICachedStoreService
    {

        [EasyCachingAble(CacheKeyPrefix = "product", Expiration = 10)]
        Product GetProduct(int id);

        [EasyCachingPut(CacheKeyPrefix = "product")]
        Product PutProduct(Product product);

        [EasyCachingEvict(CacheKeyPrefix = "product", IsBefore = true)]
        void DeleteProduct(int id);


        [EasyCachingAble(CacheProviderName = "redis", CacheKeyPrefix = "category", Expiration = 10)]
        Category GetCategory(int id);

        [EasyCachingPut(CacheProviderName = "redis", CacheKeyPrefix = "category")]
        Category PutCategory(Category category);

        [EasyCachingEvict(CacheProviderName = "redis", CacheKeyPrefix = "category", IsBefore = true)]
        void DeleteCategory(int id);
    }
}
