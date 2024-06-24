using Android.App;
using Android.Content;
using System;

namespace AG_Mobile.Models
{
    public static class ActivityExtensions
    {
        /// <summary>
        /// An extension to redirect to a new page/activity
        /// </summary>
        /// <typeparam name="T">Any type that inherits from Activity</typeparam>
        /// <param name="activity">The activity/Page</param>
        /// <param name="type">IDK WFT is this... Tehee ;P </param>
        /// <seealso cref="Activity"/>
        public static void Redirect<T>(this T activity, Type type) where T : Activity
        {
            Context context = activity.BaseContext;

            Intent intent = new Intent(context, type);
            intent.SetFlags(ActivityFlags.NewTask);

            context.StartActivity(intent);
        }
    }
}