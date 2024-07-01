using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}", Name = "GetWeatherForecastById")]
        public WeatherForecast Get(int id)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(id)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpGet("summary/{summary}", Name = "GetWeatherForecastBySummary")]
        public IEnumerable<WeatherForecast> Get(string summary)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summary
            }).ToArray();
        }

        [HttpGet("{ime}/{prezime}/{grad}", Name = "GetWeatherControllerInfoOnCity")]
        public WeatherForecast Get(string ime, string prezime, string grad)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "As predicted by " + ime + " " + prezime + " from " + grad
            };
        }

        [HttpGet("search", Name = "GetWeatherForecastByPlace")]
        public WeatherForecast GetByPlace(string town)
        {
            return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = "Weather in " + town + " is " + Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post(WeatherForecast weatherForecast)
        {
            return Ok(weatherForecast);
        }
    }
}
