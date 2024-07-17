using Microsoft.AspNetCore.Mvc;
using AssistantGenesis_Web_App.Models;

namespace AssistantGenesis_Web_App.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
            if (RESTfulClient.Instance == null)
                RESTfulClient.InitializeClient("https://localhost:7077/api/");
        }
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

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            User user = RESTfulClient.Instance.GET<User>($"Users/user={username}&key={Secrecy.HashPassword(password)}");

            if (user == null)
            {
                TempData["msg"] = "Invalid login credentials";
                TempData["success"] = false;

                return View();
            }

            TempData["msg"] = "You were succesfuly logged in";
            TempData["success"] = true;

            string sessionID = Secrecy.EncodeAES(user.Username);

            this.SetSession<User>(sessionID, user);

            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }
    }
}
