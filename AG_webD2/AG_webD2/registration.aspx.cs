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
        ServiceClient client = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        protected void btnRegister_clicked(object sender, EventArgs e)
        {
            var reg = client.Registeration(studentNum.Value, name.Value, surname.Value, username.Value, Secrecy.HashPassword(password.Value), "student");
            if (reg != null)
            {
                Console.WriteLine(reg);
                Response.Redirect("login.aspx");
            }
        }
    }
}