using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Core.Framework.Models;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly CountriesService _countriesService;

        public CountriesController(CountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<CountryResponse>))]
        public async Task<IActionResult> Index()
        {
            var countries = await _countriesService.GetCountriesAsync();

            return this.PageObject(countries);
        }

        [HttpGet("{id}")]
        [Produces(typeof(CountryResponse))]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _countriesService.GetCountryAsync(id);

            if (country == null)
                return this.ErrorObject(404, $"Country (id: {id}) is not found");

            return this.SingleObject(country);
        }
    }
}
