using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TtaApp.Shared.Application.Exceptions
{
    internal sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExceptionCompositionRoot _exceptionCompositionRoot;

        public ExceptionMiddleware(
            RequestDelegate next,
            IExceptionCompositionRoot exceptionCompositionRoot
        )
        {
            _next = next;
            _exceptionCompositionRoot = exceptionCompositionRoot;
        }

        public async Task Invoke(
            HttpContext httpContext
        )
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                var responseData = _exceptionCompositionRoot.Map(ex);

                var json = JsonConvert.SerializeObject(responseData.Response);
                httpContext.Response.StatusCode = (int)responseData.StatusCode;
                httpContext.Response.Headers.Add("content-type", "application/json");
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
