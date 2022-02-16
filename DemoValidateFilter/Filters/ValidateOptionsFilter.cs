using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoValidateFilter.Filters
{
    public class ValidateOptionsFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validatorTypes = context.ActionDescriptor.FilterDescriptors
                .Where(t => t.Filter is ValidateOptionsAttribute)
                .Select(t => t.Filter as ValidateOptionsAttribute)
                .SelectMany(t => t.ValidationTypes)
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                if (context.HttpContext.RequestServices.GetService(validatorType) is IOptions<dynamic> dynamicOptions)
                {
                    try
                    {
                        var _ = dynamicOptions.Value;
                    }
                    catch (Exception ex)
                    {
                        var result = new StatusCodeResult(502);
                        context.Result = result;
                        return;
                    }
                }
            }

            await next();
        }
    }
}
