using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Persistence;
using Store.Persistence.Identity.Contexts;
using Store.Services;
using Store.Shared.ErrorModels;
using Store.Web.Middlewares;

namespace Store.Web.Extensions {
    public static class Extensions {


        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddWebServices();
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);
            services.ConfigureApiBehaviorOptions();
            services.AddIdentityServices();

            return services;
        }

        private static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services) {

            services.Configure<ApiBehaviorOptions>(config => {
                config.InvalidModelStateResponseFactory = (ActionContext) => {

                    var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Any())
                    .Select(m => new ValidationError() {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage)
                    }).ToList();

                    var response = new ValidationErrorResponse() {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            return services;

        }

        private static IServiceCollection AddWebServices(this IServiceCollection services) {

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services) {

            services.AddIdentityCore<AppUser>(options => {

                options.User.RequireUniqueEmail = true;

            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityStoreDbContext>();

            return services;
        }



        public static async Task<WebApplication> ConfigureMiddleWares(this WebApplication app) {


            await app.SeedData();

            app.UseGlobalErrorHandling();


            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            return app;



        }

        private static async Task<WebApplication> SeedData(this WebApplication app) {
           
            var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            await dbInitializer.InitializeIdentityAsync();
           
            return app;
       
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app) {

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

    }
}
