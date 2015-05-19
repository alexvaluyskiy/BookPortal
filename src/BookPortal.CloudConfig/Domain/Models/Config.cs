namespace BookPortal.CloudConfig.Domain.Models
{
    public class Config
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int ProfileId { get; set; }
        public ConfigProfile Profile { get; set; }
    }
}
