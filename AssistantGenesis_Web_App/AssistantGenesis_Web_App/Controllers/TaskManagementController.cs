using Microsoft.AspNetCore.Mvc;

namespace AssistantGenesis_Web_App.Controllers
{
    public class TaskManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
