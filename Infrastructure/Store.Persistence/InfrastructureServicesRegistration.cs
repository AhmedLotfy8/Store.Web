using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Store.Domain.Contracts;
using Store.Persistence.Data.Contexts;
using Store.Persistence.Identity.Contexts;
using Store.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence {
    public static class InfrastructureServicesRegistration {


        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<StoreDbContext>(options => {

                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddDbContext<IdentityStoreDbContext>(options => {

                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            });
            
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>

                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"))
            );


            return services;

        }

    }
}
