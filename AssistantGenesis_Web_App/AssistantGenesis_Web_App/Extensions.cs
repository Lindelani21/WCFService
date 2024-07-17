using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssistantGenesis_Web_App
{
    public static class Extensions
    {
        private static readonly string cookieKey = "sessionID";
        /// <summary>
        /// Sets a key value, object entry in the session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="sessionID">The key for the stored object</param>
        /// <param name="obj">The object to be stored</param>
        public static void SetSession<T>(this Controller controller, string sessionID, T obj)
        {
            string jsonObj = JsonConvert.SerializeObject(obj);
            controller.HttpContext.Session.SetString(sessionID, jsonObj);

            CookieOptions options = new CookieOptions
            {
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                IsEssential = true
            };

            controller.Response.Cookies.Append(cookieKey, sessionID, options);

        }

        /// <summary>
        /// Gets an object from the session using a key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key">The key for the stored object</param>
        /// <returns>Retrieved object or null if not found</returns>
        public static T? GetSession<T>(this Controller controller)
        {
            controller.Request.Cookies.TryGetValue(cookieKey, out string sessionID);

            string? jsonObj = null;

            if (!string.IsNullOrEmpty(sessionID))
                jsonObj = controller.HttpContext.Session.GetString(sessionID);

            if (string.IsNullOrEmpty(jsonObj))
                return default;

            return JsonConvert.DeserializeObject<T>(jsonObj);
        }
    }
}
