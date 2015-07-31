using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Models.Responses;

namespace BookPortal.Web.Services
{
    public class PublishersService
    {
        private readonly BookContext _bookContext;

        public PublishersService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<PublisherResponse> GetPublisherAsync(int publisherId)
        {
            var query = from p in _bookContext.Publishers
                        join c in _bookContext.Countries on p.CountryId equals c.Id
                        where p.Id == publisherId
                        select new PublisherResponse
                        {
                            PublisherId = p.Id,
                            Name = p.Name,
                            Type = p.Type,
                            YearOpen = p.YearOpen,
                            YearClose = p.YearClose,
                            Description = p.Description,
                            DescriptionSource = p.DescriptionSource,
                            CountryId = c.Id,
                            CountryName = c.Name
                        };

            return await query.SingleOrDefaultAsync();
        }
    }
}
