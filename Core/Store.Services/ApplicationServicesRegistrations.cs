using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Services.Abstractions;
using Store.Services.Mapping.Baskets;
using Store.Services.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services {
    public static class ApplicationServicesRegistrations {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(m => m.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BasketProfile()));

            return services;

        }
    }
}
