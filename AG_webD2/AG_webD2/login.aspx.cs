using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AG_webD2.ServiceReference1;

namespace AG_webD2
{
    public partial class login : System.Web.UI.Page
    {
        //ServiceClient client = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(RESTfulClient.Instance == null)
                RESTfulClient.InitializeClent("https://localhost:7077/api/");
        }
        protected void btnLogin_Clicked(object sender, EventArgs e)
        {
            RESTfulClient client = RESTfulClient.Instance;

            User user = client.GET<User>($"Users/user={name.Value}&key={Secrecy.HashPassword(password.Value)}");

            if (user != null)
            {
                Session["User"] = user;
                Response.Redirect("home.aspx");
            }
        }
    }
}