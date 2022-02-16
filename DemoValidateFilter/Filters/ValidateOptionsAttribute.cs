using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoValidateFilter.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateOptionsAttribute : TypeFilterAttribute
    {
        public ValidateOptionsAttribute(params Type[] validationTypes) : base(typeof(ValidateOptionsFilter))
        {
            ValidationTypes = validationTypes;
        }

        public Type[] ValidationTypes { get; }
    }
}
