using AG_Mobile.Models;
using AG_Mobile.Utilities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Provider.MediaStore;

namespace AG_Mobile.Activities
{
    [Activity(Label = "Application")]
    public class Application : Activity
    {
        TextView lblSurnameInitials, lblStatus;
        RadioGroup rgAssistantType, rgModule;
        Button btnUploadTranscript, btnViewTranscript, btnSubmit, btnUploadContract, btnDownloadContract;
        ListView lvUploadedDocs;
        RESTfulClient client = RESTfulClient.Instance;
        User user;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.application);

            lblSurnameInitials = FindViewById<TextView>(Resource.Id.lblSurnameInitials);
            lblStatus = FindViewById<TextView>(Resource.Id.lblStatus);
            rgAssistantType = FindViewById<RadioGroup>(Resource.Id.rgAssistantType);
            rgModule = FindViewById<RadioGroup>(Resource.Id.rgModule);
            btnUploadTranscript = FindViewById<Button>(Resource.Id.btnUploadTranscript);
            btnViewTranscript = FindViewById<Button>(Resource.Id.btnViewTranscript);
            btnDownloadContract = FindViewById<Button>(Resource.Id.btnDownloadContract);
            btnUploadContract = FindViewById<Button>(Resource.Id.btnUploadContract);
            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            lvUploadedDocs = FindViewById<ListView>(Resource.Id.lvUploadedDocs);

            btnSubmit.Click += delegate { OnSubmitBtnClicked(); } ;

            string jsonObj = GetSharedPreferences("User", FileCreationMode.Private).GetString("User", null);
            user = JsonConvert.DeserializeObject<User>(jsonObj);
            InitializeView();
        }

        private async void InitializeView()
        {
            string initials = "";
            foreach (string name in user.Name.Split(" "))
            {
                initials += name.First();
            }

            lblSurnameInitials.Text = $"{user.Surname} {initials}";
            // send.Attributes["hidden"] = "true";

            Models.Application application = client.GET<Models.Application>($"Applications/student={user.Id}");   // client.GetApplication(user.Id);
            if (application != null)
            {
                //  heading.InnerHtml = "Application";

                // If not pending then shouldn't be modifiable
                if (application.Status.ToLower() != "pending")
                {
                    btnUploadTranscript.Visibility = ViewStates.Gone;

                    for (int i = 0; i < rgAssistantType.ChildCount; i++)
                    {
                        var option = rgAssistantType.GetChildAt(i);
                        option.Enabled = false;

                        if (option is RadioButton op)
                            op.Checked = op.Text.ToLower() == application.Role.ToLower() ? true : false;
                    }

                    for (int i = 0; i < rgModule.ChildCount; i++)
                    {
                        var option = rgModule.GetChildAt(i);
                        option.Enabled = false;

                        if (option is RadioButton op)
                            op.Checked = op.Text.ToLower() == application.Module.ToLower() ? true : false;
                    }
                }

                // Hide submit button
                btnSubmit.Visibility = ViewStates.Gone;
                //transcript.Attributes["hidden"] = "true";
                //fileUpload.Attributes["hidden"] = "true";
                //transcript.Attributes.Remove("required");
                //btnApply.Attributes["hidden"] = "true";
                //role.Attributes["disabled"] = "true";
                //module.Attributes["disabled"] = "true";
                //status.Attributes["disabled"] = "true";


                //rgAssistantType.se = application.Role;
                //module.Value = application.Module;
                lblStatus.Text = application.Status;

                List<string> docs = new List<string>();
                docs.Add(application.Transcript.Split('\\').Last());

                //documents.InnerHtml = "<h1>Uploaded Documents</h1>" + application.Transcript.Split('\\').Last();
                //documents.InnerHtml = "<h1>Uploaded Documents</h1>" + $"</br> <a class='application' href='viewDocument.aspx?docId=0' onClick='viewDoc'>{application.Transcript.Split('\\').Last()}</a>";

                Document signed = await client.GET_Async<Document>($"Documents/user={user.Id}&type={"signed"}"); // client.GetDocumentByUser(user.Id, "signed");
                if (signed != null)
                    docs.Add(signed.Path.Split('\\').Last());
                //documents.InnerHtml += $"<br/> <a class='application' href='viewDocument.aspx?docId={signed.ID}' onClick='viewDoc'>{signed.Path.Split('\\').Last()}</a>";

                ArrayAdapter adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, docs);
                lvUploadedDocs.Adapter = adapter;

                // Contract stuff
                Document contract = await client.GET_Async<Document>($"Documents/user={user.Id}&type={"contract"}"); //client.GetDocumentByUser(user.Id, "contract");
                if (contract == null)
                {
                    btnDownloadContract.Visibility = ViewStates.Gone;
                    btnUploadContract.Visibility = ViewStates.Gone;
                    return;
                }
                // documents.InnerHtml += $"<br/> {contract.Path.Split('\\').Last()}";
                // transcript.Attributes.Remove("hidden");
                return;
            }

            btnDownloadContract.Visibility = ViewStates.Gone;
            btnUploadContract.Visibility = ViewStates.Gone;
            lblStatus.Visibility = ViewStates.Gone;

            //download.Attributes["hidden"] = "true";
            //upload.Attributes["hidden"] = "true";
            //status.Attributes["hidden"] = "true";
        }

        public async void OnSubmitBtnClicked()
        {
            Models.Application application = new Models.Application
            {
                Id = user.Id,
                Module = FindViewById<RadioButton>(rgModule.CheckedRadioButtonId).Text,
                Role = FindViewById<RadioButton>(rgAssistantType.CheckedRadioButtonId).Text,
                Status = "Pending",
                Transcript = "Testing.Transcript.pdf"
            };

            if(client.POST("Applications", application))
                RunOnUiThread(() => { Toast.MakeText(this, "Application was successful", ToastLength.Long).Show(); });
            else
                RunOnUiThread(() => { Toast.MakeText(this, "Application was unsuccessful!!!", ToastLength.Long).Show(); });
        }
    }
}