using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Middleware
{
    public class ApiKeyMiddleware
    {
        const string APIKEY = "O1m4vOFwV6chFgtMtPwFcZPcoUJNnJxehVqeX6pHvKkKUJsm4LjWBSdL8pY0On2Y";

        private readonly RequestDelegate next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api key was not provided!");
                return;
            }

            if (!APIKEY.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized!");
                return;
            }

            await next(context);
        }
    }
}
