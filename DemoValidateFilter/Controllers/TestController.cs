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
}
