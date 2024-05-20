using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class applications : System.Web.UI.Page
    {
        ServiceClient client = new ServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null && ((User)Session["User"]).Role != "lecturer")
                return;

            Application[] applications = client.GetApplications();
            if (applications == null)
                return;

            foreach (Application application in applications)
            {
                User student = client.GetUser(application.Id);
                string initials = "";

                foreach (string initial in student.Name.Split(' '))
                {
                    initials += initial.ToUpper().ElementAt(0);
                }
                applicationsDiv.InnerHtml += $"<a class='application' href='apply.aspx?studentID={application.Id}'><b>{student.Surname} {initials}</b>&#160 &#160Assistant Type: {application.Role}\tModule: {application.Module}</a> <br>";
            }
        }
    }
}