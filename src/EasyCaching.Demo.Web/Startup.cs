namespace EasyCaching.Demo.Interceptors
{
    using EasyCaching.Core;
    using EasyCaching.Demo.Interceptors.Services;
    using EasyCaching.Interceptor.AspectCore;
    using EasyCaching.Redis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

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

            services.AddEasyCaching(options =>
            {
                options.UseInMemory("memory");

                options.UseRedis(config =>
                {
                    config.DBConfig = new RedisDBOptions { Configuration = "localhost" };
                    config.SerializerName = "json";
                }, "redis")
                .WithJson();
            });

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = EasyCachingConstValue.DefaultInMemoryName);
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