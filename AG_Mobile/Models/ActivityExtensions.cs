using Android.App;
using Android.Content;
using System;

namespace AG_Mobile.Models
{
    public static class ActivityExtensions
    {
        public static void Redirect<T>(this T activity, Type type) where T : Activity
        {
            Context context = activity.BaseContext;

            Intent intent = new Intent(context, type);
            intent.SetFlags(ActivityFlags.NewTask);

            context.StartActivity(intent);
        }
    }
}