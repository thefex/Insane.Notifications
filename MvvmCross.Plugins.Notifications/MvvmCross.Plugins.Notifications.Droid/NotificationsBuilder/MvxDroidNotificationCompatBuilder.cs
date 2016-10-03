using Android.App;
using Android.Content;
using Android.Support.V4.App;

namespace MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder
{
    public abstract class MvxDroidNotificationCompatBuilder<TNotificationData> : IMvxDroidNotificationBuilder<TNotificationData> where TNotificationData : class
    {
        public Notification BuildNotification(Context context, TNotificationData notificationData)
        {
            return 
                ConfigureNotificationBuilder(context, notificationData, new NotificationCompat.Builder(context))
                .Build();
        }

        protected abstract NotificationCompat.Builder ConfigureNotificationBuilder(Context context, TNotificationData notificationData, NotificationCompat.Builder notificationBuilder);
    }
}