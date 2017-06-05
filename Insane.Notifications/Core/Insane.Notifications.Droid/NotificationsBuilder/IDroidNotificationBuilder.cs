using Android.App;
using Android.Content;

namespace MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder
{
    public interface IDroidNotificationBuilder<in TNotificationData> where TNotificationData : class
    {
        Notification BuildNotification(Context context, TNotificationData notificationData);
    }
}