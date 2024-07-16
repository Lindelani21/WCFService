using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Net;
using AG_Mobile.Utilities;
using System.IO;
using Android.Graphics;
using AG_Mobile.Models;
using Newtonsoft.Json;

namespace AG_Mobile.Activities
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class Home : AppCompatActivity
    {
        protected Button loginTab;
        protected Button logoutTab;
        protected Button applicationTab;
        protected Button profileTab;
        private User user;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.home);
         
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RESTfulClient.InitializeClent(Resources.GetString(Resource.String.base_url));

            // This is like the session thingie
            string jsonObj = GetSharedPreferences("User", FileCreationMode.Private).GetString("User", null);
            user = string.IsNullOrEmpty(jsonObj) ? null : JsonConvert.DeserializeObject<User>(jsonObj);

            loginTab = FindViewById<Button>(Resource.Id.loginTab);
            logoutTab = FindViewById<Button>(Resource.Id.logoutTab);
            profileTab = FindViewById<Button>(Resource.Id.profileTab);
            applicationTab = FindViewById<Button>(Resource.Id.applicationTab);
            UpdateViews();

            // This is self explainatory
            loginTab.Click += delegate { this.Redirect(typeof(Login)); };   // Function is in models -> ActivityExtensions. This is what needs fixing
            logoutTab.Click += delegate { this.DeleteSharedPreferences("User"); this.Redirect(typeof(Login)); };
            profileTab.Click += delegate { };
            applicationTab.Click += delegate { this.Redirect(typeof(Application)); };
        }

        /// <summary>
        /// Displays and hides view according to user role
        /// </summary>
        private void UpdateViews()
        {
            if (user != null)
            {
                logoutTab.Visibility = ViewStates.Visible;
                profileTab.Visibility = ViewStates.Visible;

                switch (user.Role.ToLower())
                {
                    case "student":
                        applicationTab.Visibility = ViewStates.Visible;
                        break;
                    case "assistant":
                        break;
                    default:
                        loginTab.Visibility = ViewStates.Visible;
                        logoutTab.Visibility= ViewStates.Gone;
                        profileTab.Visibility = ViewStates.Gone;
                        applicationTab.Visibility = ViewStates.Gone;
                        break;

                }
            }
            else
                this.Redirect(typeof(Login));
        }

        // I don't remember what this is
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

