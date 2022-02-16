using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoValidateFilter.Filters
{
    public class ValidateOptionsFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor action) 
            {
                var methodAttribute = action.MethodInfo.GetCustomAttribute<ValidateOptionsAttribute>();
                var classAttribute = action.MethodInfo.DeclaringType.GetCustomAttribute<ValidateOptionsAttribute>();
                var finalAttribute = methodAttribute ?? classAttribute;

                if (finalAttribute != null)
                {
                    var validatorTypes = finalAttribute.ValidationTypes;
                    foreach (var validatorType in validatorTypes)
                    {
                        if (context.HttpContext.RequestServices.GetService(validatorType) is IOptions<dynamic> dynamicOptions)
                        {
                            try
                            {
                                var _ = dynamicOptions.Value;
                            }
                            catch(Exception ex)
                            {
                                var result = new StatusCodeResult(502);
                                context.Result = result;
                                return;
                            }
                        }

                    }
                }
            }

            await next();
        }
    }
}
