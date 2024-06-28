using AssistantGenesis_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssistantGenesis_Web_App.Controllers
{
    public class ApplicationReviewController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Application> applications = RESTfulClient.Instance.GET<IEnumerable<Application>>("Applications/");

            if(applications == default)
            {
                string message = $"applications is null, please make sure you have the correct implementation and have applications in your system.";
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
            }

            return View("ApplicationReview", applications);
        }
    }
}
