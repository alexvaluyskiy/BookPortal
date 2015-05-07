using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BookPortal.Web.Models;
using BookPortal.Web.Services;
using HtmlAgilityPack;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImportersController : Controller
    {
        private readonly ImporterService _importerService;

        public ImportersController(ImporterService importerService)
        {
            _importerService = importerService;
        }

        [HttpGet("ozon/{bookid}")]
        public async Task<IActionResult> ImportOzon(int bookid)
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(new Uri("http://www.ozon.ru/context/detail/id/"), bookid.ToString());

                HttpResponseMessage response = await client.SendAsync(request);
                string html = await response.Content.ReadAsStringAsync();

                var importEdition = _importerService.ParseOzonPage(html);
                return new WrappedObjectResult(importEdition);
            }
        }
    }
}
