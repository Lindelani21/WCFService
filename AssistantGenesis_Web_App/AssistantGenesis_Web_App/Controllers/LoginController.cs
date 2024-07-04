using Microsoft.AspNetCore.Mvc;

namespace AssistantGenesis_Web_App.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            if (RESTfulClient.Instance == null)
                RESTfulClient.InitializeClient("https://localhost:7077/api/");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
