using System.Threading.Tasks;
using BookPortal.Logging.Domain;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Logging.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly LogsContext _logsContext;

        public LogsController(LogsContext logsContext)
        {
            _logsContext = logsContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Log value)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest("Log model is not valid");

            _logsContext.Logs.Add(value);
            try
            {
                await _logsContext.SaveChangesAsync();
            }
            catch
            {
                return new HttpStatusCodeResult(409);
            }

            return new HttpStatusCodeResult(202);
        }
    }
}
