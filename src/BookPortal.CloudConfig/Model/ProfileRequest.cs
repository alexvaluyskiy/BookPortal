using System.ComponentModel.DataAnnotations;

namespace BookPortal.CloudConfig.Model
{
    public class ProfileRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
