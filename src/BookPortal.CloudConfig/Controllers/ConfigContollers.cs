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
            int profileId = _context.Profiles.Where(c => c.Name == profile).Select(c => c.Id).SingleOrDefault();

            if (profileId == 0)
                return this.ErrorObject(400, $@"Profile ""{profile}"" is not found");

            var configs = await _context.Configs
                .Where(c => c.ProfileId == profileId)
                .Select(c => new KeyValuePair<string, string>(c.Key, c.Value))
                .ToListAsync();

            return new ObjectResult(configs);
        }
    }
}
