#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [Route("weather")]
    public class WeatherController : Controller
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        // GET: /Weather/
        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: 
        [Route("search")]
        [HttpGet]
        // Weather API only updates every 10 minutes
        [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Search(string? query)
        {
            if (ModelState.IsValid)
            {
                return await Task.FromResult(new ViewComponentResult()
                {
                    ViewComponentName = "WeatherForecast",
                    Arguments = query
                });
            }

            return ViewComponent("WeatherForecast", "");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

#nullable restore