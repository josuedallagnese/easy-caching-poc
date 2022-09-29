﻿using EasyCaching.Core.Interceptor;
using Newtonsoft.Json;
using System;

namespace EasyCaching.Demo.Interceptors.Services
{
    public abstract class Entity : ICachable
    {
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        
        [JsonIgnore]
        public string CacheKey { get; }

        public Entity()
        {
        }

        public Entity(int id)
        {
            Id = id;
            Creation = DateTime.Now;

            CacheKey = id.ToString();
        }

        public override string ToString() => $"{GetType().Name} {Id} - {Creation:HH:mm:ss}";
    }
}