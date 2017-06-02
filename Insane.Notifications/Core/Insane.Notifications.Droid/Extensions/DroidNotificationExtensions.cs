using System;
using Android.App;
using Android.Content;

namespace MvvmCross.Plugins.Notifications.Droid.Extensions
{
    public static class DroidNotificationExtensions
    {
        static int id = int.MinValue;

        public static void Show(this Notification notification, Context context)
        {
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            int notificationid = System.Threading.Interlocked.Increment(ref id);
            notificationManager.Notify(notificationid, notification);
        }
    }
}
