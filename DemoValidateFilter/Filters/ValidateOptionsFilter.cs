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
                var attribute = action.MethodInfo.GetCustomAttribute<ValidateOptionsAttribute>();
                if (attribute != null)
                {
                    var validatorTypes = attribute.ValidationTypes;
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
