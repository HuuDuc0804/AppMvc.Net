using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class PlanetController : Controller
    {
        private readonly ILogger<PlanetController> _logger;
        private readonly PlanetService _planetService;

        public PlanetController(ILogger<PlanetController> logger, PlanetService planetService)
        {
            _logger = logger;
            _planetService = planetService;
        }
        [Route("danh-sach-cac-hanh-tinh.html")]
        public IActionResult Index()
        {
            return View();
        }
        [BindProperty(SupportsGet = true, Name = "action")]
        public string? Name { get; set; } // Action ~ Planet
        public IActionResult Mercury()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Earth()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Jupiter()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Mars()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Neptune()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        public IActionResult Saturn()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }

        [Route("sao/[controller]/[action]", Order = 1, Name = "Uranus")] // => sao/Planet/Uranus
        [Route("[controller]-[action].html", Order = 2)] // => Planet-Uranus.html
        public IActionResult Uranus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        [Route("sao/[action]")] // => sao/Venus
        public IActionResult Venus()
        {
            var planet = _planetService.Where(p => p.Name == Name).FirstOrDefault();
            return View("Detail", planet);
        }
        [Route("hanh-tinh/{id:int}")] // => hanh-tinh/1
        public IActionResult PlanetInfo(int Id)
        {
            var planet = _planetService.Where(p => p.Id == Id).FirstOrDefault();
            return View("Detail", planet);
        }
    }
}