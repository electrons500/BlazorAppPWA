using BlazorAppPWA.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingDBController : ControllerBase
    {
        private GameService _gameService;
        public PingDBController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("PingDatabase")]
        public ActionResult PingDatabase()
        {
            bool result = _gameService.PingDatabase();
            return result ? Ok("true") : BadRequest("false");
        }
    }
}
