using AG_Mobile.Models;
using AG_Mobile.Utilities;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Net;

namespace AG_Mobile.Activities
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        private Button btnLogin;

        ISharedPreferences sharedPrefs;
        ISharedPreferencesEditor editor;
        private TextView lnkRegister;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.login);

            // Set logo image
            ImageView imgLogo = FindViewById<ImageView>(Resource.Id.imgLogo);
            imgLogo.SetImageBitmap(this.GetImageAsset("ag_logo"));

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            sharedPrefs = GetSharedPreferences("User", FileCreationMode.Private);
            editor = sharedPrefs.Edit();

            //Thread loginThread = new Thread(onBtnLoginClicked) { IsBackground = true };

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += delegate
            {
                onBtnLoginClicked();
            };
            //{
            //   if (loginThread.ThreadState != ThreadState.Running)
            //        loginThread.Start();
            //   else
            //    {
            //        loginThread.Abort();
            //        loginThread.Start();
            //    }
            //};

            lnkRegister = FindViewById<TextView>(Resource.Id.lnkRegister);
            lnkRegister.Click += delegate { this.Redirect(typeof(Register)); };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected async void onBtnLoginClicked()
        {
            string username = FindViewById<AutoCompleteTextView>(Resource.Id.txtUsername).Text;
            string password = Secrecy.HashPassword(FindViewById<AutoCompleteTextView>(Resource.Id.txtPassword).Text);

            RESTfulClient client = RESTfulClient.Instance;

            //User user = client.GET<User>($"Users/user={username}&key={password}");
            User user = await client.GET_Async<User>($"Users/user={username}&key={password}");

            RunOnUiThread(() => { Toast.MakeText(BaseContext, client.Message, ToastLength.Long).Show(); });

            if (user != null)
            {
                if (user.Role.ToLower() != "student" && user.Role.ToLower() != "assistant")
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, $"This is no place for {user.Role}s XD", ToastLength.Long).Show();
                    });
                    return;
                }

                string jsonObj = JsonConvert.SerializeObject(user);

                editor.PutString("User", jsonObj);

                editor.Apply();

                RunOnUiThread(new Action(() => { Toast.MakeText(this, "You have successful logged in!", ToastLength.Long).Show(); this.Redirect(typeof(Home)); }));
            }
            else
                RunOnUiThread( () => { Toast.MakeText(this, "Your login was unsuccessful! Make sure your credentials are correct.", ToastLength.Long).Show(); });
                
        }
    }
}

