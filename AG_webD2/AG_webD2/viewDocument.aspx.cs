using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class viewDocument : System.Web.UI.Page
    {
        string path;
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
                        documentDiv.InnerHtml = $"\n<iframe src='{application.Transcript}' style='width=100%; height=100%'></iframe>";
                        path = application.Transcript;
                    }
                    else
                    {
                        document = client.GET<Document>($"Documents/{documentID}");
                        documentDiv.InnerHtml = $"\n<iframe src='{document.Path}' style='width=100%; height=100%'></iframe>";
                        path = document.Path;
                    }
                    break;

                case "lecturer":

                    if (documentID == 0)
                    {
                        application = client.GET<Application>($"Applications/student={Request.QueryString["studentId"]}");
                        documentDiv.InnerHtml = $"\n<iframe src='{application.Transcript}' style='width=100%; height=100%'></iframe>";
                        path = application.Transcript;
                    }
                    else
                    {
                        document = client.GET<Document>($"Documents/{documentID}");
                        documentDiv.InnerHtml = $"\n<iframe src='{document.Path}' style='width=100%; height=100%'></iframe>";
                        path = document.Path;
                    }
                    break;
            }
            // Check the document Id
        }

        protected void btnDownload_Clicked(object sender, EventArgs e)
        {
            string filePath = Server.MapPath(path);
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", $"attachment; filename={path.Split('\\').Last()}");
            Response.WriteFile(filePath);
            Response.End();
        }
    }
}