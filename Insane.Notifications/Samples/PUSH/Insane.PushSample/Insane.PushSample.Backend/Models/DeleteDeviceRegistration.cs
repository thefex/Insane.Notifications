using System.ComponentModel.DataAnnotations;

namespace Insane.PushSample.Backend.Models
{
    public class DeleteDeviceRegistration
    {
        [Required]
        public string Id { get; set; }
    }
}