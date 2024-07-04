using AssistantGenesis_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssistantGenesis_Web_App.Controllers
{
    public class ApplicationReviewController : Controller
    {
        //creating an instance of an application
        public static Application? SelectedApplication { get; set; }

        //method returning an IActionREsult
        public IActionResult Index()
        {
            //IEnumerable - collection of items that can be itterated through 
            //.GET<IEnumerable<Application>> - getting a collection of applications from the API
            //"Application/" is an API endpoint
            IEnumerable<Application> applications = RESTfulClient.Instance.GET<IEnumerable<Application>>("Applications/");

            //what is the purpose of checking if the applications is defaut?
            if(applications == default)

                //create new list of applications
                return View(new List<Application>());

            SelectedApplication = applications.ElementAt(0);

            return View(applications);
        }
    }
}
