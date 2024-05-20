using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace AG_Mobile
{
    [Activity(Label = "Activity1")]
    public class Register : Activity
    {
        private Button btnRegister;
        private string message;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.register);

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            Thread loginThread = new Thread(onBtnRegisterClicked) { IsBackground = true };

            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            btnRegister.Click += delegate
            {
                loginThread.Start();
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected void onBtnRegisterClicked()
        {
            string username = FindViewById<AutoCompleteTextView>(Resource.Id.txtUsername).Text;
            string password = FindViewById<AutoCompleteTextView>(Resource.Id.txtPassword).Text;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://10.0.2.2:7077/api/Users");

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Read response
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    StringBuilder resp = new StringBuilder();
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        resp.Append(line).Append("\n");
                    }
                    reader.Close();

                    // Print response
                    System.Console.WriteLine("Response from RESTful service:");
                    System.Console.WriteLine(resp.ToString());

                    message = resp.ToString();

                    // Use Jsoon object

                }
                else
                {
                    message = ("Failed to call function. Response code: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                message = ($"Connection failed: {ex.Message}");
            }

        }
    }
}