using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestAPI.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private const string API_KEY_NAME = "X-AUF-API-KEY";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "API Key was not provided"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
            var apiKey = appSettings!.GetValue<string>(API_KEY_NAME);

            if (!apiKey!.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Client."
                };
                return;
            }
        }
    }
}
