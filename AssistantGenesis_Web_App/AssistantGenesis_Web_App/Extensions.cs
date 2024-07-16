using Newtonsoft.Json;

namespace AssistantGenesis_Web_App
{
    public static class Extensions
    {
        /// <summary>
        /// Sets a key value, object entry in the session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key">The key for the stored object</param>
        /// <param name="obj">The object to be stored</param>
        public static void Set<T>(this ISession session, string key, T obj)
        {
            string jsonObj = JsonConvert.SerializeObject(obj);
            session.SetString(key, jsonObj);
        }

        /// <summary>
        /// Gets an object from the session using a key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key">The key for the stored object</param>
        /// <returns>Retrieved object or null if not found</returns>
        public static T? Get<T>(this ISession session, string key)
        {
            string? jsonObj = session.GetString(key);

            if (string.IsNullOrEmpty(jsonObj))
                return default;

            return JsonConvert.DeserializeObject<T>(jsonObj);
        }
    }
}
