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
            }
            catch (Exception ex) {

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

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
