using BlogApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.Attributes;


// essa classe serve mais como uma chave de indetificação caso não queira fazer uma authenticação mais detalhada com Claims
[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyName, out var extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey não encontrada"
            };
            return;
        }
        
        if (!Configuration.ApiKey.Equals(extractedApiKey))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Acesso não autorizado"
            };
            return;
        }

        await next();
    }
}