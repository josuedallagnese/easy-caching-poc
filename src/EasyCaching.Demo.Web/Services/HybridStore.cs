using EasyCaching.Core;
using System;
using EasyCaching.Demo.Web.Services.Entities;

namespace EasyCaching.Demo.Web.Services
{
    public class HybridStore : IHybridStore
    {
        private readonly Lazy<IEasyCachingProvider> _easyMemoryCachingProvider;
        private readonly Lazy<IEasyCachingProvider> _easyRedisCachingProvider;
        private readonly IHybridCachingProvider _hybridCachingProvider;

        public HybridStore(IEasyCachingProviderFactory factory, IHybridCachingProvider hybridCachingProvider)
        {
            _easyMemoryCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("memory"));
            _easyRedisCachingProvider = new Lazy<IEasyCachingProvider>(factory.GetCachingProvider("redis"));
            _hybridCachingProvider = hybridCachingProvider;
        }

        public void DeleteCustomer(int id)
        {
        }

        public Customer GetCustomer(int id)
        {
            return new Customer(id);
        }

        public Customer GetCustomerByHybrid(int id)
        {
            var cachedCustomer = _hybridCachingProvider.Get<Customer>(
                $"customer:{id}",
                () =>
                {
                    // Load data from database
                    return new Customer(id);
                },
                TimeSpan.FromMinutes(5));

            return cachedCustomer?.Value;
        }

        public Customer GetCustomerByMemory(int id)
        {
            var cachedCustomer = _easyMemoryCachingProvider.Value.Get<Customer>($"customer:{id}");

            return cachedCustomer?.Value;
        }

        public Customer GetCustomerByRedis(int id)
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
