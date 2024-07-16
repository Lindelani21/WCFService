using Android.App;
using Android.Content;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace AG_Mobile.Utilities
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

        /// <summary>
        /// An extension to read an image file as a bitmap
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activity"></param>
        /// <param name="img">The image assset name i.e filename w/o the extension</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Thrown when the image asset is not found</exception>
        public static Bitmap GetImageAsset<T>(this T activity, string img) where T : Activity
        {
            string? imgFile = activity.Assets.List("Images/").Where(image => image.ToLower().Contains(img.ToLower())).FirstOrDefault();

            if(!string.IsNullOrEmpty(imgFile))
            {
                Stream stream = activity.Assets.Open($"Images/{imgFile}");

                const int size = 5 * 1024;
                List<byte[]> buffers = new List<byte[]>();
                buffers.Add(new byte[size]);

                int index = 0;
                while (stream.Read(buffers.ElementAt(index), 0, size) == size)
                {
                    buffers.Add(new byte[size]);
                    index++;
                }

                byte[] bytes = new byte[size * buffers.Count()];

                for (int i = 0; i <= index; i++)
                    Array.Copy(buffers.ElementAt(i), 0, bytes, i * size, size);

                return (BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length));
            }
            else
            {
                StackTrace stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(0);

                throw new FileNotFoundException($"The image specified could not be found in {frame.GetFileName()}; line: {frame.GetFileLineNumber()}; column: {frame.GetFileColumnNumber()}");
            }
            
        }
    }
}