using Common.Contract.RequestsAndResponses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<CreateRoomRequest> Get([FromQuery] CreateRoomRequest request)
        {
            var gameRoom = request.CreateRoom;
            return BadRequest("Some exception");
        }

        //[HttpGet(Name = "Fpp")]
        //public ActionResult Fpp()
        //{
        //    return null;
        //}
    }
}