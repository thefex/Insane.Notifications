using Android.App;
using Android.Content;
using Android.Support.V4.App;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;

namespace MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications
{
    class LocalNotificationDataNotificationBuilder : MvxDroidNotificationCompatBuilder<LocalNotificationData>
    {
        protected override NotificationCompat.Builder ConfigureNotificationBuilder(Context context, LocalNotificationData notificationData,
            NotificationCompat.Builder notificationBuilder)
        {
	        var mainActivityIntent = new Intent(context, typeof(MainActivity));
	        var pendingIntent = PendingIntent.GetActivity(context, 123, mainActivityIntent,
		        PendingIntentFlags.UpdateCurrent);

	        return notificationBuilder
				.SetSmallIcon(Resource.Drawable.Icon)
		        .SetContentTitle("Test Notification")
		        .SetContentText(notificationData.Title)
				.SetContentIntent(pendingIntent)
				.SetAutoCancel(false);
        }
    }
}