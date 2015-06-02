using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookPortal.CloudConfig.Domain;
using Microsoft.AspNet.Mvc;

namespace BookPortal.CloudConfig.Controllers
{
    public class ConfigContollers : Controller
    {
        private readonly ConfigContext _context;

        public ConfigContollers(ConfigContext context)
        {
            _context = context;
        }

        [HttpGet("api/config/{profile}")]
        public async Task<IActionResult> Get(string profile)
        {
            var configs = await _context.Configs.Where(c => c.Profile.Name == profile).ToListAsync();

            if (configs == null)
                return this.ErrorObject(400, $"Can't find the profile: {profile}");

            return this.SingleObject(200, configs);
        }
    }
}
