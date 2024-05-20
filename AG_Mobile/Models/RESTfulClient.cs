using Android.App;
using Android.Content;
using Android.Content.Res;
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

namespace AG_Mobile.Models
{
    public class RESTfulClient
    {
        private string baseURL;
        private string message;
        private static RESTfulClient instance;

        private RESTfulClient(string baseURL)
        {
            this.baseURL = baseURL;
            this.message = string.Empty;
        }

        public static void InitializeClent(string baseURL)
        {
            if (string.IsNullOrEmpty(baseURL))
                return;

            baseURL = baseURL.Last() == '/' ? baseURL : baseURL + '/';

            instance = new RESTfulClient(baseURL);
        }

        public static void InitializeClent(Activity activity)
        {
            InitializeClent(activity.Resources.GetString(Resource.String.base_url));
        }

        public static RESTfulClient Instance { get { return instance; } }

        public string Message { get => message;}

        public T GET<T>(string directive)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{baseURL}{directive}");
            string JsonObj;

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
                    response.Close();

                    JsonObj = resp.ToString();

                    message = "API call was successful";

                    // Use Json object
                    return JsonConvert.DeserializeObject<T>(JsonObj);

                }
                else
                {
                     message = ("API call failed. Response code: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                message = ($"Connection failed: {ex.Message}");
                return default;
            }

            return default;
        }
    }
}