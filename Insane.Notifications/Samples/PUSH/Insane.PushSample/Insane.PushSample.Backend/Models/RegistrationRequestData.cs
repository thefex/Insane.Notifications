using System.ComponentModel.DataAnnotations;

namespace Insane.PushSample.Backend.Models
{
    public class RegistrationRequestData
    {
        [Required]
        public string Handle { get; set; }
    }
}