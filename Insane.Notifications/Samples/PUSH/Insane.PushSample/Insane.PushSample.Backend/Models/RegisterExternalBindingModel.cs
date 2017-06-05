using System.ComponentModel.DataAnnotations;

namespace Insane.PushSample.Backend.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}