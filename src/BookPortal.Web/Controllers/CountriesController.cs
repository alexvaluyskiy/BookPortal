using System.Collections.Generic;
using System.Threading.Tasks;
using BookPortal.Web.Models.Responses;
using BookPortal.Web.Services;
using Microsoft.AspNet.Mvc;
using Swashbuckle.Swagger.Annotations;

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

            return this.PageObject(countries.Values, countries.TotalRows);
        }

        [HttpGet("{id}")]
        [Produces(typeof(CountryResponse))]
        [SwaggerResponse(404, "Country is not found")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _countriesService.GetCountryAsync(id);

            if (country == null)
                return this.ErrorObject(404, $"Country (id: {id}) is not found");

            return this.SingleObject(country);
        }
    }
}
