using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation
{
    public class RedisCashAttribute(int durationInSec = (180)) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            string cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetAsync(cacheKey);
            if (!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult
                {
                    Content = result,
                    ContentType = "Application/Json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }
            var contextResult = await next.Invoke();

            if (contextResult.Result is OkObjectResult Result)
            {
                //var jsonValue = System.Text.Json.JsonSerializer.Serialize(okObject.Value);
                await cacheService.SetAsync(cacheKey, Result.Value, TimeSpan.FromSeconds(durationInSec));
            }


        }

        private  string GenerateCacheKey(HttpRequest request)
        {
            // {baseurl}/api/products?{query1=value1}&{query2=value2}
            StringBuilder cacheKey = new StringBuilder();
            cacheKey.Append(request.Path + '?');
            foreach (var query in request.Query.OrderBy(Q => Q.Key))
            {
                cacheKey.Append($"{query.Key}={query.Value}&");
            }

            return cacheKey.ToString();

        }
    }
}
