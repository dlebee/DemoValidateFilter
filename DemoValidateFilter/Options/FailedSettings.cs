using System.ComponentModel.DataAnnotations;

namespace DemoValidateFilter.Options
{
    public class FailedSettings
    {
        [Required]
        public string Message { get; set; }
    }
}
