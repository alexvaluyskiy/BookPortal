﻿using System.Threading.Tasks;
using BookPortal.Core.ApiPrimitives;
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
        public async Task<IActionResult> Index()
        {
            var countries = await _countriesService.GetCountriesAsync();

            return this.PageObject(200, countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await _countriesService.GetCountryAsync(id);

            if (country == null)
                return this.ErrorObject(404, $"Country (id: {id}) is not found");

            return this.SingleObject(200, country);
        }
    }
}
