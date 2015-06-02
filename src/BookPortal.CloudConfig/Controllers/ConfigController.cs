using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using BookPortal.CloudConfig.Domain;
using BookPortal.CloudConfig.Domain.Models;
using BookPortal.CloudConfig.Model;
using Microsoft.AspNet.Mvc;

namespace BookPortal.CloudConfig.Controllers
{
    public class ConfigController : Controller
    {
        private readonly ConfigContext _context;

        public ConfigController(ConfigContext context)
        {
            _context = context;
        }

        [HttpGet("api/config/{profile}")]
        public async Task<IActionResult> Get(string profile)
        {
            int profileId = GetProfileId(profile);

            if (profileId == 0)
                return this.ErrorObject(400, $@"Profile ""{profile}"" is not found");

            var configs = await _context.Configs
                .Where(c => c.ProfileId == profileId)
                .Select(c => new KeyValuePair<string, string>(c.Key, c.Value))
                .ToListAsync();

            return new ObjectResult(configs);
        }

        [HttpPost("api/config/{profile}")]
        public async Task<IActionResult> Post(string profile, [FromBody]ConfigRequest request)
        {
            int profileId = GetProfileId(profile);

            if (profileId == 0)
                return this.ErrorObject(400, $@"Profile ""{profile}"" is not found");

            Config config = GetConfig(profileId, request.Key);

            if (config == null)
            {
                config = new Config { Key = request.Key, Value = request.Value, ProfileId = profileId };
                _context.Add(config);
            }
            else
            {
                _context.Update(config);
            }

            await _context.SaveChangesAsync();

            return new ObjectResult(request) { StatusCode = 201 };
        }

        [HttpDelete("api/config/{profile}/{key}")]
        public async Task<IActionResult> Delete(string profile, string key)
        {
            int profileId = GetProfileId(profile);
            if (profileId == 0)
                return this.ErrorObject(400, $@"Profile ""{profile}"" is not found");

            var config = _context.Configs.SingleOrDefault(c => c.ProfileId == profileId && c.Key == key);
            if (config == null)
                return this.ErrorObject(400, $@"Key ""{key}"" is not found");

            _context.Configs.Remove(config);
            await _context.SaveChangesAsync();

            return new HttpStatusCodeResult(204);
        }

        private int GetProfileId(string profile)
        {
            return _context.Profiles.Where(c => c.Name == profile).Select(c => c.Id).SingleOrDefault();
        }

        private Config GetConfig(int profileId, string key)
        {
            return _context.Configs.SingleOrDefault(c => c.ProfileId == profileId && c.Key == key);
        }
    }
}
