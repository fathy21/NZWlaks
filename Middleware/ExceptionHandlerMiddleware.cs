using System.Net;

namespace NZWalks.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger 
            , RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //logging the exception 
                logger.LogError(ex , $"{errorId} : {ex.Message}");

                // return the custom error Response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;  // ex=>500
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErorrMessage = "something went wrong! we are looking into resolving the problem."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        } 
    }
}
