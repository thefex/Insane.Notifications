using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.Plugins.Notifications.Droid.LocalNotifications;
using NotificationsSample.Portable.Data;

namespace DroidNotificationsSample.Services.LocalNotifications
{
    // Add [BroadcastReceiver] attribute so your AndroidManifest gets updated.
    [BroadcastReceiver]
    public class AppLocalNotificationsPeriodicUpdateAlarmReceiver : MvxPeriodicUpdateAlarmReceiver<AppLocalNotificationsService, LocalNotificationData>
    {
        public AppLocalNotificationsPeriodicUpdateAlarmReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public AppLocalNotificationsPeriodicUpdateAlarmReceiver()
        {
        }
    }
}