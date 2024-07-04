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
            //use the restful client and call on the API and check if it is null or not and then do the validation thing
            int res = dbop.LoginCheck(ad);
            if (res == 1)
            {
                //user should be stored in a session variable 
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
