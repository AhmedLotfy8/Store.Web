
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Contracts;
using Store.Persistence;
using Store.Persistence.Data.Contexts;
using Store.Services;
using Store.Services.Abstractions;
using Store.Services.Mapping.Products;
using Store.Shared.ErrorModels;
using Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.Web {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options => {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new ProductProfile(builder.Configuration)));

            builder.Services.Configure<ApiBehaviorOptions>(config => {
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




            var app = builder.Build();





            #region Initialize Db

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            #endregion


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();


            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
