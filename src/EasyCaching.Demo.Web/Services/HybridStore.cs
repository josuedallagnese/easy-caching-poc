using EasyCaching.Core;
using System;
using EasyCaching.Demo.Web.Services.Entities;

namespace EasyCaching.Demo.Web.Services
{
    public class HybridStore : IHybridStore
    {
        private readonly Lazy<IEasyCachingProvider> _easyMemoryCachingProvider;
        private readonly Lazy<IEasyCachingProvider> _easyRedisCachingProvider;

        public HybridStore(IEasyCachingProviderFactory factory)
        {
            _easyMemoryCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("memory"));
            _easyRedisCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("redis"));
        }

        public void DeleteCustomer(int id)
        {
        }

        public Customer GetCustomer(int id)
        {
            return new Customer(id);
        }

        public Customer GetCustomerInMemory(int id)
        {
            var cachedCustomer = _easyMemoryCachingProvider.Value.Get<Customer>($"customer:{id}");

            return cachedCustomer?.Value;
        }

        public Customer GetCustomerInRedis(int id)
        {
            var cachedCustomer = _easyRedisCachingProvider.Value.Get<Customer>($"customer:{id}");

            return cachedCustomer?.Value;
        }

        public Customer PutCustomer(Customer customer)
        {
            return customer;
        }
    }
}
