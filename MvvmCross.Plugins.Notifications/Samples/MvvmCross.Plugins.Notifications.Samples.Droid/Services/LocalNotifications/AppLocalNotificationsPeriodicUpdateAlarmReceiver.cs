using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.Plugins.Notifications.Droid.LocalNotifications;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;

namespace MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications
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