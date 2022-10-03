using EasyCaching.Core.Interceptor;
using EasyCaching.Demo.Web.Services.Entities;

namespace EasyCaching.Demo.Web.Services
{
    public interface IHybridStore
    {
        Customer GetCustomerByMemory(int id);
        Customer GetCustomerByRedis(int id);
        Customer GetCustomerByHybrid(int id);


        [EasyCachingAble(CacheKeyPrefix = "customer", IsHybridProvider = true, Expiration = 10)]
        Customer GetCustomer(int id);

        [EasyCachingPut(CacheKeyPrefix = "customer", IsHybridProvider = true)]
        Customer PutCustomer(Customer customer);

        [EasyCachingEvict(CacheKeyPrefix = "customer", IsHybridProvider = true, IsBefore = true)]
        void DeleteCustomer(int id);
    }
}
