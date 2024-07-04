using Microsoft.AspNetCore.Mvc;
using AssistantGenesis_Web_App.Models;

namespace AssistantGenesis_Web_App.Controllers
{
    public class LoginController : Controller
    {
        db dbop = new db();

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult MyIndex([Bind] login ad)
        {
            int res = dbop.LoginCheck(ad);
            if (res == 1)
            {
                TempData["msg"] = "Welcome";
            }
            else
            {
                TempData["msg"] = " Invalid login credentials";
            }
            return View();
        }
    }
}
