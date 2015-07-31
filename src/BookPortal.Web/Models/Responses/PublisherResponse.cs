using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Models.Responses
{
    public class PublisherResponse
    {
        public int PublisherId { get; set; }

        public string Name { get; set; }

        public PublisherType? Type { get; set; }

        public int? YearOpen { get; set; }

        public int? YearClose { get; set; }

        public string Description { get; set; }

        public string DescriptionSource { get; set; }

        public int? CountryId { get; set; }

        public string CountryName { get; set; }
    }
}
