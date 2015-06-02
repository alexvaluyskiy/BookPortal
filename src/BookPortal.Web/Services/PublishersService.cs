using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.Web.Domain;
using BookPortal.Web.Domain.Models;

namespace BookPortal.Web.Services
{
    public class PublishersService
    {
        private readonly BookContext _bookContext;

        public PublishersService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<Publisher> GetPublisherAsync(int id)
        {
            return await _bookContext.Publishers.Where(c => c.Id == id).SingleOrDefaultAsync();
        }
    }
}
