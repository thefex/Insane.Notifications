using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;

namespace MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications
{
    class LocalNotificationDataNotificationBuilder : MvxDroidNotificationCompatBuilder<LocalNotificationData>
    {
        protected override Type GetPendingIntentActivityType()
        {
            return typeof(MainActivity);
        }

        protected override NotificationCompat.Builder ConfigureNotificationBuilder(Context context, LocalNotificationData notificationData,
            NotificationCompat.Builder notificationBuilder)
        {
	        return notificationBuilder
				.SetSmallIcon(Resource.Drawable.Icon)
		        .SetContentTitle("Test Notification")
		        .SetContentText(notificationData.Title)
				.SetAutoCancel(false);
        }
    }
}