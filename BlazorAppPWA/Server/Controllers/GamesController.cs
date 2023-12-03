using BlazorAppPWA.Server.Service;
using BlazorAppPWA.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorAppPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private GameService _gameService;
        public GamesController(GameService gameService)
        {
            _gameService = gameService;
        }
        // GET: api/<GamesController>
        [HttpGet("GetGames")]
        public ActionResult GetGames()
        {
            var gamelist = _gameService.GetGames();
            return Ok(gamelist);
        }

        // GET api/<GamesController>/5
        [HttpGet("GetGame/{id}")]
        public ActionResult GetGame(int id)
        {
            var game = _gameService.GetGame(id);
            return Ok(game);
        }

        // POST api/<GamesController>
        [HttpPost("AddGame")]
        public ActionResult AddGame([FromBody] Game game)
        {
            bool result = _gameService.AddGame(game);
            return result ? Ok("Game successfully added") : BadRequest();
        }

        // PUT api/<GamesController>/5
        [HttpPut("UpdateGame/{id}")]
        public ActionResult UpdateGame([FromBody] Game game) 
        {
            bool result = _gameService.UpdateGame(game);
            return result ? Ok("Game successfully updated") : BadRequest();
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("DeleteGame/{id}")]
        public ActionResult DeleteGame(int id)
        {
            bool result = _gameService.DeleteGame(id);
            return result ? Ok("Game successfully deleted") : BadRequest();
        }
    }
}
