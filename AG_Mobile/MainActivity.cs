using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Net;
using AG_Mobile.Models;

namespace AG_Mobile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ISharedPreferences sharedPrefs;

        protected Button loginTab;
        protected Button logoutTab;
        protected Button applicationTab;
        protected Button profileTab;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            RESTfulClient.InitializeClent(Resources.GetString(Resource.String.base_url));

            // This is like the session thingie
            sharedPrefs = GetSharedPreferences("User", FileCreationMode.Private);

            loginTab = FindViewById<Button>(Resource.Id.loginTab);
            logoutTab = FindViewById<Button>(Resource.Id.logoutTab);
            profileTab = FindViewById<Button>(Resource.Id.profileTab);
            applicationTab = FindViewById<Button>(Resource.Id.applicationTab);
            UpdateViews();

            // This is self explainatory
            loginTab.Click += delegate { this.Redirect(typeof(Login)); };
            logoutTab.Click += delegate { this.DeleteSharedPreferences("User"); this.RunOnUiThread(() => { UpdateViews(); }); };
            profileTab.Click += delegate { };
            applicationTab.Click += delegate { };
        }

        /// <summary>
        /// Displays and hides view according to user role
        /// </summary>
        private void UpdateViews()
        {
            if (sharedPrefs.All.Count != 0)
            {
                logoutTab.Visibility = ViewStates.Visible;
                profileTab.Visibility = ViewStates.Visible;

                string role = sharedPrefs.GetString("Role", string.Empty);

                switch (role)
                {
                    case "student":
                        applicationTab.Visibility = ViewStates.Visible;
                        break;
                    case "assistant":
                        break;
                    default:
                        if(!string.IsNullOrEmpty(role))
                            Toast.MakeText(this, $"This is no place for {role}s XD", ToastLength.Long).Show();

                        loginTab.Visibility = ViewStates.Visible;
                        logoutTab.Visibility= ViewStates.Gone;
                        profileTab.Visibility = ViewStates.Gone;
                        applicationTab.Visibility = ViewStates.Gone;
                        break;

                }
            }
            else
                loginTab.Visibility = ViewStates.Visible;
        }

        // I don't remember what this is
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

