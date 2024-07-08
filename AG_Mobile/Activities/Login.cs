using AG_Mobile.Models;
using AG_Mobile.Utilities;
using Android.App;
using Android.Content;
using Android.Graphics;
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

namespace AG_Mobile.Activities
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

            // Set logo image
            ImageView imgLogo = FindViewById<ImageView>(Resource.Id.imgLogo);
            Stream stream = Assets.Open("Images/ag_logo.png");

            const int size = 5 * 1024;
            List<byte[]> buffers = new List<byte[]>();
            buffers.Add(new byte[size]);

            int index = 0;
            while(stream.Read(buffers.ElementAt(index), 0, size) == size)
            {
                buffers.Add(new byte[size]);
                index++;
            }

            byte[] bytes = new byte[size * buffers.Count()];

            for(int i=0; i <= index; i++)
                Array.Copy(buffers.ElementAt(i), 0, bytes, i * size, size);
       
            imgLogo.SetImageBitmap(BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length));

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
                editor.PutInt("Id", user.Id);
                editor.PutString("Name", user.Name);
                editor.PutString("Surname", user.Surname);
                editor.PutString("Role", user.Role);

                editor.Apply();

                RunOnUiThread(new Action(() => { Toast.MakeText(this, "You have successful logged in!", ToastLength.Long).Show(); this.Redirect(typeof(Home)); }));
            }
            else
                RunOnUiThread( () => { Toast.MakeText(this, "Your login was unsuccessful! Make sure your credentials are correct.", ToastLength.Long).Show(); });
                
        }
    }
}

