using Azure;
using E_Commerce.App.Application.Exception;

using E_Commerce_Api.Controller.Error;
using System.Net;

namespace E_Commerce.APIs.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                #region Loggin TODO
                if (_env.IsDevelopment())
                {
                    // Development Mode

                    _logger.LogError(ex, ex.Message);

                }
                else
                {
                    // Production Mode
                } 
                #endregion

                await HandelExceptionAsync(httpContext, ex);

            }
        }

        private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(404, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                case ValidationExeption validationExeption:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiValidationsErrorResponse(ex.Message) { Errors = validationExeption.Errors};

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;


                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(400, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case UnAuthorizedExeption:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(401, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                default:

                    response = _env.IsDevelopment()?
                               new ApiExceptionResponse((int) HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                               :
                               new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync(response.ToString());

                    break;

            }
        }
    }
}
