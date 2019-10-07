using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        [Required]
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "This is not a valid US postal code.")]
        public string RequestZip { get; set; }
    }
}