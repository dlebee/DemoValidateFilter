using DemoValidateFilter.Filters;
using DemoValidateFilter.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DemoValidateFilter.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [ValidateOptions(typeof(IOptions<FailedSettings>))]
        public IActionResult Fail([FromServices]IOptions<FailedSettings> settings) => Ok(settings.Value.Message);
        [ValidateOptions(typeof(IOptions<SuccessSettings>))]
        public IActionResult Win([FromServices]IOptions<SuccessSettings> settings) => Ok(settings.Value.Message);
    }

    [ApiController]
    [Route("[controller]")]
    [ValidateOptions(typeof(IOptions<FailedSettings>))]
    public class Test2Controller : Controller
    {
        private readonly IOptions<FailedSettings> options;

        public Test2Controller(IOptions<FailedSettings> options)
        {
            this.options = options;
        }
        
        public IActionResult WontWork() => Ok(options.Value.Message);
    }

    [ApiController]
    [Route("[controller]")]
    [ValidateOptions(typeof(IOptions<SuccessSettings>))]
    public class Test3Controller : Controller
    {
        private readonly IOptions<SuccessSettings> options;

        public Test3Controller(IOptions<SuccessSettings> options)
        {
            this.options = options;
        }

        public IActionResult WillWork() => Ok(options.Value.Message);
    }
}
