using System.ComponentModel.DataAnnotations;

namespace BookPortal.CloudConfig.Model
{
    public class ConfigRequest
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
