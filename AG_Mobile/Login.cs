using AG_Mobile.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace AG_Mobile
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        private Button btnLogin;
        private string message;

        ISharedPreferences sharedPrefs;
        ISharedPreferencesEditor editor;
        private TextView lnkRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.login);

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            sharedPrefs = GetSharedPreferences("User", FileCreationMode.Private);
            editor = sharedPrefs.Edit();

            Thread loginThread = new Thread(onBtnLoginClicked) { IsBackground = true };

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {
               if (loginThread.ThreadState != ThreadState.Running)
                    loginThread.Start();
               else
                {
                    loginThread.Abort();
                    loginThread.Start();
                }
            };

            lnkRegister = FindViewById<TextView>(Resource.Id.lnkRegister);
            lnkRegister.Click += delegate { this.Redirect(typeof(Register)); };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected void onBtnLoginClicked()
        {
            string username = FindViewById<AutoCompleteTextView>(Resource.Id.txtUsername).Text;
            string password = Secrecy.HashPassword(FindViewById<AutoCompleteTextView>(Resource.Id.txtPassword).Text);

            RESTfulClient client = RESTfulClient.Instance;

            User user = client.GET<User>($"Users/user={username}&key={password}");
            RunOnUiThread(() => { Toast.MakeText(BaseContext, client.Message, ToastLength.Long).Show(); });

            if (user != null)
            {
                editor.PutInt("Id", user.Id);
                editor.PutString("Name", user.Name);
                editor.PutString("Surname", user.Surname);
                editor.PutString("Role", user.Role);

                editor.Apply();

                RunOnUiThread(new Action(() => { Toast.MakeText(this, "You have successful logged in!", ToastLength.Long).Show(); this.Redirect(typeof(MainActivity)); }));
            }
            else
                RunOnUiThread( () => { Toast.MakeText(this, "Your login was unsuccessful! Make sure your credentials are correct.", ToastLength.Long).Show(); });
                
        }
    }
}

