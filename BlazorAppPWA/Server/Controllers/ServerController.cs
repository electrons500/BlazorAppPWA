using BlazorAppPWA.Server.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorAppPWA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private SqlQueryService _sqlQueryService;
        public ServerController(SqlQueryService sqlQueryService)
        {
            _sqlQueryService = sqlQueryService;
        }

        [HttpPost("SendQuery")]
        public async Task<ActionResult> SendQuery([FromBody] string query)
        {
            bool result = await _sqlQueryService.ExecuteSqlQueryFromLocalDB(query);
            return result ? Ok() : BadRequest();
        }
       
    }
}
