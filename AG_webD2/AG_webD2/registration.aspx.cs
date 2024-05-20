using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class registration : System.Web.UI.Page
    {
        //ServiceClient client = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnRegister_clicked(object sender, EventArgs e)
        {
            RESTfulClient client = RESTfulClient.Instance;

            User user = new User
            {
                StudentNum = studentNum.Value,
                Name = name.Value,
                Surname = surname.Value,
                Username = username.Value,
                Password = Secrecy.HashPassword(password.Value),
                Role = "student"
            };

            bool isRegistered = client.POST("Users", user);

            //var reg = client.Registeration(studentNum.Value, name.Value, surname.Value, username.Value, Secrecy.HashPassword(password.Value), "student");

            if (isRegistered) //reg != null)
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}