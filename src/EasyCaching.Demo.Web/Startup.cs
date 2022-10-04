using EasyCaching.Demo.Interceptors.Services;
using EasyCaching.Demo.Web.Services;
using EasyCaching.Interceptor.AspectCore;
using EasyCaching.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyCaching.Demo.Interceptors
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IHybridStore, HybridStore>();

            services.AddEasyCaching(options =>
            {
                options.UseInMemory(config =>
                {
                    config.EnableLogging = true;
                }, "memory");

                options.UseRedis(config =>
                {
                    config.EnableLogging = true;
                    config.DBConfig = new RedisDBOptions { Configuration = "localhost:6379,allowAdmin=true,defaultDatabase=0,abortConnect=false" };
                    config.SerializerName = "json";
                }, "redis");

                options.UseHybrid(config =>
                {
                    config.EnableLogging = true;
                    config.LocalCacheProviderName = "memory";
                    config.DistributedCacheProviderName = "redis";
                    config.TopicName = "cache-topic";
                }, "hybrid");

                options.WithRedisBus(config =>
                {
                    config.Endpoints.Add(new Core.Configurations.ServerEndPoint("localhost", 6379));
                    config.AbortOnConnectFail = false;
                    config.SerializerName = "json";
                }, "redis-bus");

                options.WithJson();
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "hybrid");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
