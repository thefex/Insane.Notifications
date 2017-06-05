using System.ComponentModel.DataAnnotations;

namespace Insane.PushSample.Backend.Models
{
    public class DeviceRegistrationUpdate
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public PushPlatformType? Platform { get; set; }

        [Required]
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}