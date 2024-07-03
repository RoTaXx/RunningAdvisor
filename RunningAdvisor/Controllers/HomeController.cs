using Microsoft.AspNetCore.Mvc;
using RunningAdvisor.Models;
using RunningAdvisor.Services;
using System.Diagnostics;

namespace RunningAdvisor.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeatherService _weatherService;

        public HomeController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            var weather = await _weatherService.GetCurrentWeatherAsync("Sofia");
            ViewBag.Weather = weather;
            return View();
        }
    }
}
