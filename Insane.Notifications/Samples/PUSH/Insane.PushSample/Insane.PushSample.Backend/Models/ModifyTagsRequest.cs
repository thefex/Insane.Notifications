using System.ComponentModel.DataAnnotations;

namespace Insane.PushSample.Backend.Models
{
    public class ModifyTagsRequest
    {
        [Required]
        public string[] Tags { get; set; }
    }
}