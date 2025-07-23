using Microsoft.AspNetCore.Mvc;
using Quack.Application.Interfaces;

namespace Quack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogParserService logParserService;

        public GamesController(ILogParserService logParserService)
        {
            this.logParserService = logParserService;
        }

        [HttpPost("reportLog")]
        public IActionResult ReportLog([FromBody] string content)
        {
            var logLines = content.Split(new[] { '\n', '\r' }).ToList();

            var result = logParserService.ReadLines(logLines);

            return Ok(result);
        }
    }
}
