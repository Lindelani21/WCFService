using AG_webD2.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AG_webD2
{
    public partial class apply : System.Web.UI.Page
    {
        //ServiceClient client = new ServiceClient();
        RESTfulClient client = RESTfulClient.Instance;

        string rootDir = ".\\Files\\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
                Response.Redirect("login.aspx");

            User user = ((User)Session["User"]);

            switch (user.Role.ToLower())
            {
                case "student":
                case "assistant":
                {
                    studentNum.Value = user.StudentNum;

                    send.Attributes["hidden"] = "true";

                    Application application = client.GET<Application>($"Applications/student={user.Id}");//client.GetApplication(user.Id);
                    if (application != null)
                    {
                        heading.InnerHtml = "Application";
                        transcript.Attributes["hidden"] = "true";
                        fileUpload.Attributes["hidden"] = "true";
                        transcript.Attributes.Remove("required");
                        btnApply.Attributes["hidden"] = "true";
                        role.Attributes["disabled"] = "true";
                        module.Attributes["disabled"] = "true";
                            status.Attributes["disabled"] = "true";


                        role.Value = application.Role;
                        module.Value = application.Module;
                        status.Value = application.Status;

                            documents.InnerHtml = "<h1>Uploaded Documents</h1>" + application.Transcript.Split('\\').Last();
                            documents.InnerHtml = "<h1>Uploaded Documents</h1>" + $"</br> <a class='application' href='viewDocument.aspx?docId=0' onClick='viewDoc'>{application.Transcript.Split('\\').Last()}</a>";

                            Document signed = client.GET<Document>($"Documents/user={user.Id}&type={"signed"}"); // client.GetDocumentByUser(user.Id, "signed");
                            if (signed != null)
                                documents.InnerHtml += $"<br/> <a class='application' href='viewDocument.aspx?docId={signed.ID}' onClick='viewDoc'>{signed.Path.Split('\\').Last()}</a>";

                            // Contract stuff
                            Document contract= client.GET<Document>($"Documents/user={user.Id}&type={"contract"}"); //client.GetDocumentByUser(user.Id, "contract");
                            if (contract == null)
                        {
                                download.Attributes["hidden"] = "true";
                                upload.Attributes["hidden"] = "true";
                                break;
                        }
                            //documents.InnerHtml += $"<br/> {contract.Path.Split('\\').Last()}";
                                transcript.Attributes.Remove("hidden");
                            break;
                    }

                        download.Attributes["hidden"] = "true";
                        upload.Attributes["hidden"] = "true";
                        status.Attributes["hidden"] = "true";
                    break;
                }
                case "lecturer":
                {
                    if (Request.QueryString["studentID"] == null)
                        Response.Redirect("applications.aspx");

                    int studentID = int.Parse(Request.QueryString["studentID"]);
                    Application application = client.GET<Application>($"Applications/student={studentID}");//client.GetApplication(studentID);

                    User applicant = client.GET<User>($"Users/{studentID}"); // client.GetUser(int.Parse(Request.QueryString["studentID"]));
                    string initials = "";

                    foreach (string initial in user.Name.Split(' '))
                    {
                        initials += initial.ToUpper().ElementAt(0);
                    }

                        heading.InnerHtml = "Application";
                        role.Attributes["disabled"] = "true";
                        module.Attributes["disabled"] = "true";
                        transcript.Attributes.Remove("required");
                        documents.InnerHtml = "<h1>Uploaded Documents</h1>" + $"</br> <a class='application' href='viewDocument.aspx?docId=0&studentId={applicant.Id}' onClick='viewDoc'>{application.Transcript.Split('\\').Last()}</a>";
                        upload.Attributes["hidden"] = "true";
                        btnApply.Attributes["hidden"] = "true";
                        updateStatus.Attributes.Remove("hidden");

                        Document signed = client.GET<Document>($"Documents/user={applicant.Id}&type={"signed"}"); //client.GetDocumentByUser(applicant.Id, "signed");
                        if (signed != null)
                            documents.InnerHtml += $"<br/> <a class='application' href='viewDocument.aspx?docId={signed.ID}' onClick='viewDoc'>{signed.Path.Split('\\').Last()}</a>";

                        studentNum.Value = $"{applicant.StudentNum}\t{applicant.Surname} {initials}";
                    role.Value = application.Role;
                    module.Value = application.Module;

                    if(!IsPostBack)
                        status.Value = application.Status;

                        // Contract stuff
                        Document contract = client.GET<Document>($"Documents/user={applicant.Id}&type={"contract"}"); //client.GetDocumentByUser(applicant.Id, "contract");
                        if (contract == null || contract.Type != "contract")
                        {
                            download.Attributes["hidden"] = "true";
                        }

                        break;
                }

            }
        }

        protected void btnApply_Clicked(object sender, EventArgs e)
        {
            if (transcript.PostedFile != null)
            {
                //var aply = client.apply(role.Value, module.Value, transcript.PostedFile.FileName, ((User)Session["User"]).Id, status.Value);

                User user = ((User)Session["User"]);
                string initials = "";

                foreach (string initial in user.Name.Split(' '))
                {
                    initials += initial.ElementAt(0);
                }

                Application application = new Application
                {
                    Role = role.Value, 
                    Module = module.Value, 
                    Status = status.Value, 
                    Id = ((User)Session["User"]).Id, 
                    Transcript = $"{rootDir}{user.Surname}{initials}{transcript.PostedFile.FileName}"
                };

                bool isApplied = client.POST("Applications", application);

                if (!isApplied)
                {
                    Response.Redirect("apply.aspx");
                    return;
                }

                // Save file
                transcript.PostedFile.SaveAs(Server.MapPath(application.Transcript));
                Response.Redirect("apply.aspx");
            }
        }

        protected void btnSend_Clicked(object sender, EventArgs e)
        {
            if (transcript.PostedFile != null)
            {
                User applicant = client.GET<User>($"Users/{Request.QueryString["studentID"]}"); // client.GetUser(int.Parse(Request.QueryString["studentID"]));
                string initials = "";

                foreach (string initial in applicant.Name.Split(' '))
                {
                    initials += initial.ElementAt(0);
                }

                string filePath = $"{rootDir}{applicant.Surname}{initials}{transcript.PostedFile.FileName}";

                Document document = new Document
                {
                    userID = applicant.Id,
                    Type = "contract",
                    Path = filePath,
                    TimeStamp = DateTime.Now
                };

                // Save file
                if (client.POST("Documents", document)) //client.CreateDocument(applicant.Id, "contract", filePath, DateTime.Now))
                {
                    transcript.PostedFile.SaveAs(Server.MapPath(filePath));
                    Response.Redirect("apply.aspx");
                }
            }
        }

        protected void btnDownload_Clicked(object sender, EventArgs e)
        {
            User user = ((User)Session["User"]);
            Document contract = client.GET<Document>($"Documents/user={user.Id}&type={"contract"}"); //client.GetDocumentByUser(user.Id, "contract");
            if (contract != null)
            {
                string filePath = Server.MapPath(contract.Path);
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"attachment; filename={contract.Path.Split('\\').Last()}");
                Response.WriteFile(filePath);
                Response.End();
            }
        }

        protected void btnUpload_Clicked(object sender, EventArgs e)
        {
            User user = ((User)Session["User"]);

            if (transcript.PostedFile != null)
            {
                string initials = "";

                foreach (string initial in user.Name.Split(' '))
                {
                    initials += initial.ElementAt(0);
                }

                string filePath = $"{rootDir}{user.Username}{initials}{transcript.PostedFile.FileName.Replace(".pdf", string.Empty)}_Signed.pdf";

                // Save file
                Document document = new Document
                { 
                    userID = user.Id,
                    Type = "signed",
                    Path = filePath,
                    TimeStamp = DateTime.Now
                };

                if ( client.POST("Documents", document)) //client.CreateDocument(user.Id, "signed", filePath, DateTime.Now))
                {
                    transcript.PostedFile.SaveAs(Server.MapPath(filePath));
                }              
            }
            Response.Redirect("apply.aspx");
        }

        protected void onStatusChanged(object sender, EventArgs e)
        {
            User applicant = client.GET<User>($"Users/{Request.QueryString["studentID"]}");// client.GetUser(int.Parse(Request.QueryString["studentID"]));
            Application application = client.GET<Application>($"Applications/student={applicant.Id}");//client.GetApplication(applicant.Id);

            if (application.Status != status.Value)
            {
                application.Status = status.Value;  // CHange
                //if(client.UpdateApplication(application) && application.Status.ToLower() == "hired")
                if(client.PUT($"Applications/{application.ApplicationId}", application) && application.Status.ToLower() == "hired")
                {
                    applicant.Role = "assistant";
                    //client.updateUserRole(applicant.Id, applicant.Role);
                    client.PUT($"User/{applicant.Id}", applicant);
                }
            }
        }
    }
}