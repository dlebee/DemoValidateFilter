using System.ComponentModel.DataAnnotations;

namespace DemoValidateFilter.Options
{
    public class SuccessSettings
    {
        [Required]
        public string Message { get; set; }
    }
}
