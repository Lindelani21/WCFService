using AssistantGenesis_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssistantGenesis_Web_App.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            User? user = HttpContext.Session.IsAvailable?this.GetSession<User>():null;

            if (user == null)
            {
                string message = "You are currently not logged in.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
            }

            ViewData["User"] = user;

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
    }
}
