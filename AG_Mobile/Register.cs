using AG_Mobile.Models;
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
            string name = FindViewById<AutoCompleteTextView>(Resource.Id.txtName).Text;
            string surname = FindViewById<AutoCompleteTextView>(Resource.Id.txtSurname).Text;
            string studentNo = FindViewById<AutoCompleteTextView>(Resource.Id.txtStudentNo).Text;
            string username = FindViewById<AutoCompleteTextView>(Resource.Id.txtUsername).Text;
            string password = FindViewById<AutoCompleteTextView>(Resource.Id.txtPassword).Text;
            string passwordC = FindViewById<AutoCompleteTextView>(Resource.Id.txtPasswordC).Text;

            if(!password.Equals(passwordC))
            {
                Toast.MakeText(this, "Passwords do not match!", ToastLength.Long);
                FindViewById<AutoCompleteTextView>(Resource.Id.txtPassword).Text = string.Empty;
                FindViewById<AutoCompleteTextView>(Resource.Id.txtPasswordC).Text = string.Empty;
                return;
            }

            User user = new User
            {
                Name = name,
                Surname = surname,
                StudentNum = studentNo,
                Username = username,
                Password = Secrecy.HashPassword(password)
            };

            if(RESTfulClient.Instance.POST("Users", user))
            {
                RunOnUiThread(() => { Toast.MakeText(this, "You have registered successfuly!", ToastLength.Long).Show(); });
                this.Redirect(typeof(MainActivity));
            }
                
        }
    }
}