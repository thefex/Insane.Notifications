using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;
using NotificationsSample.Portable.Data;

namespace DroidNotificationsSample.Services.LocalNotifications
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