using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementModelLayer.Models.Auth
{
    public class GoogleLoginRequest
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}
