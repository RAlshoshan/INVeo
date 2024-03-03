using INVeo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INVeo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult InnovatorPageTemp()
        {
            return View();
        }
        public IActionResult InvestorPageTemp()
        {
            //var ideas = _context.Ideas.ToList(); // Assuming _context is your DbContext
            //return View(ideas);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ViewContract()
        {
            return View();
        }
    }
}