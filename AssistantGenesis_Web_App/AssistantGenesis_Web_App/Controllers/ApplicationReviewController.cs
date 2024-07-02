using AssistantGenesis_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssistantGenesis_Web_App.Controllers
{
    public class ApplicationReviewController : Controller
    {
        public static Application? SelectedApplication { get; set; }

        public IActionResult Index()
        {
            IEnumerable<Application> applications = RESTfulClient.Instance.GET<IEnumerable<Application>>("Applications/");

            if(applications == default)
                return View(new List<Application>());

            SelectedApplication = applications.ElementAt(0);

            return View(applications);
        }
    }
}
