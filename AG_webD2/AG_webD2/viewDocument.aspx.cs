using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class viewDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RESTfulClient client = RESTfulClient.Instance;

            // Check type of user
            User user = (User)Session["User"];

            if (user == null || string.IsNullOrEmpty(Request.QueryString["docId"]))
                Response.Redirect("login.aspx");

            int documentID = int.Parse(Request.QueryString["docId"]);
            Application application = null;
            Document document = null;

            switch (user.Role)
            {
                case "student":
                case "assistant":
                    if (documentID == 0)
                    {
                        application = client.GET<Application>($"Applications/student={user.Id}");
                        documentDiv.InnerHtml = $"\n<iframe src='{application.Transcript}'></iframe>";
                    }
                    else
                    {
                        document = client.GET<Document>($"Documents/{documentID}");
                    }
                    break;

                case "lecturer":

                    break;
            }
            // Check the document Id
        }
    }
}