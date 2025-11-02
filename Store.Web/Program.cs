
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
using Store.Web.Extensions;
using Store.Web.Middlewares;
using System.Threading.Tasks;

namespace Store.Web {
    public class Program {
        public static async Task Main(string[] args) {


            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAllServices(builder.Configuration);


            var app = builder.Build();


            await app.ConfigureMiddleWares();




            app.Run();
        }
    }
}
