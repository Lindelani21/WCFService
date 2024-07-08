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
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AG_Mobile.Utilities
{
    public class RESTfulClient
    {
        private string baseURL = null;
        private string message = null;
        private static RESTfulClient instance = null;

        private RESTfulClient(string baseURL)
        {
            this.baseURL = baseURL;
            this.message = string.Empty;
        }

        /// <summary>
        /// Initializes the base URL of the RESTful API client
        /// </summary>
        /// <param name="baseURL">Base URL e.g. https://localhost:{port}/</param>
        public static void InitializeClent(string baseURL)
        {
            if (string.IsNullOrEmpty(baseURL))
                return;

            baseURL = baseURL.Last() == '/' ? baseURL : baseURL + '/';

            instance = new RESTfulClient(baseURL);
        }

        /// <summary>
        /// The instance of the RESTful API client. Null if not initialized
        /// </summary>
        public static RESTfulClient Instance { get { return instance; } }

        public string Message { get => message; }

        /// <summary>
        /// Retrieves an existing object from the web api
        /// </summary>
        /// <typeparam name="T">Type of the returned object</typeparam>
        /// <param name="directive">Relative path for the api call e.g "Products/{id}"</param>
        /// <returns>Retrieved object</returns>
        public T GET<T>(string directive)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{baseURL}{directive}");
            request.Method = "GET";
            request.ContentType = "application/json";
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

                    response.Close();

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

        public async Task<T> GET_Async<T>(string directive)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback += delegate { return true; };

            HttpClient httpClient = new HttpClient(httpClientHandler);

            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{baseURL}{directive}");
            httpResponse.EnsureSuccessStatusCode();

            string jsonObj = await httpResponse.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonObj);
        }

        /// <summary>
        /// Creates an entry in the database
        /// </summary>
        /// <typeparam name="T">Type of the object to be sent to the api</typeparam>
        /// <param name="directive">Relative path for the api call e.g "Products/{id}</param>
        /// <param name="data">object to be sent/posted</param>
        /// <returns>true - success, false - fail</returns>
        public bool POST<T>(string directive, T data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{baseURL}{directive}");
            request.Method = "POST";
            request.ContentType = "application/json";

            string jsonObj = JsonConvert.SerializeObject(data);
            const string regex = ",?\"([a-zA-Z]+)?[iI][dD]\"\\s?:\\s?0,?\r\n";
            jsonObj = Regex.Replace(jsonObj, regex, string.Empty);

            byte[] bytes = Encoding.UTF8.GetBytes(jsonObj);

            try
            {
                // Write to request body
                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    message = "API call was successful";

                    return true;
                }
                else
                {
                    message = ("API call failed. Response code: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                message = ($"Connection failed: {ex.Message}");
                return false;
            }

            return false;
        }

        /// <summary>
        /// Updates an entry in the database
        /// </summary>
        /// <typeparam name="T">Type of the object sent to the API</typeparam>
        /// <param name="directive">Relative path for the api call e.g "Products/{id}</param>
        /// <param name="data">The updated object to update the entry in the database</param>
        /// <returns></returns>
        public bool PUT<T>(string directive, T data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{baseURL}{directive}");
            request.Method = "PUT";
            request.ContentType = "application/json";

            string jsonObj = JsonConvert.SerializeObject(data);

            byte[] bytes = Encoding.UTF8.GetBytes(jsonObj);

            try
            {
                // Write to request body
                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    message = "API call was successful";

                    return true;
                }
                else
                {
                    message = ("API call failed. Response code: " + response.StatusCode);
                }
            }
            catch (WebException ex)
            {
                message = ($"Connection failed: {ex.Message}");
                return false;
            }

            return false;
        }

    }
}