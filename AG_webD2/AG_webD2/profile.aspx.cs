using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class profile : System.Web.UI.Page
    {
        ServiceClient client = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("login.aspx");

            // All we needed was this
            if (!IsPostBack)
            {
                studentNum.Value = ((User)Session["User"]).StudentNum;
                name.Value = ((User)Session["User"]).Name;
                surname.Value = ((User)Session["User"]).Surname;
                username.Value = ((User)Session["User"]).Username;
                password.Value = ((User)Session["User"]).Password;

                studentNum.Attributes["readOnly"] = "true";
                name.Attributes["readOnly"] = "true";
                surname.Attributes["readOnly"] = "true";
                username.Attributes["readOnly"] = "true";
                password.Attributes["readOnly"] = "true";
            }
        }

        protected void btnEdit_clicked(object sender, EventArgs e)
        {       
            name.Attributes.Remove("readOnly");
            surname.Attributes.Remove("readOnly");
            username.Attributes.Remove("readOnly");
            password.Attributes.Remove("readOnly");

        }

        protected void btnDelete_clicked(object sender, EventArgs e)
        {
            if (client.deleteUser(((User)Session["User"]).StudentNum))
            {
                Session.Remove("User");
                Response.Redirect("login.aspx");
            }
            else
            {
                Console.WriteLine("Unsuccessful");
            }
        }

        protected void btnSave_clicked(object sender, EventArgs e)
        {
            
            var updatedUser = client.updateUser(studentNum.Value, name.Value, surname.Value, username.Value, Secrecy.HashPassword(password.Value));
            if (updatedUser != null)
            {
                Session["User"] = updatedUser;
            }
        }

    }
}