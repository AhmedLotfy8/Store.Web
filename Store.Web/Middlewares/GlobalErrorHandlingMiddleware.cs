using Microsoft.AspNetCore.Http.HttpResults;
using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Shared.ErrorModels;

namespace Store.Web.Middlewares {
    public class GlobalErrorHandlingMiddleware {

        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context) {

            try {

                await _next.Invoke(context);

                if (context.Response.StatusCode == 404) {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails() {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"Endpoint {context.Request.Path} was not found!"
                    };

                    await context.Response.WriteAsJsonAsync(response);

                }

            }
            catch (Exception ex) {

                context.Response.StatusCode = ex switch {

                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError

                };

                context.Response.ContentType = "application/json";

                var response = new ErrorDetails() {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message,
                };

                await context.Response.WriteAsJsonAsync(response);

            }

        }



    }
}
