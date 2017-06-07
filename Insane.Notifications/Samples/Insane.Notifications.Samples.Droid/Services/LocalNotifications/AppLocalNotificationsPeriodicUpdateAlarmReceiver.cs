using System;
using Android.Content;
using Android.Runtime;
using Insane.Notifications.Droid.Local;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;

namespace MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications
{
    // Add [BroadcastReceiver] attribute so your AndroidManifest gets updated.
    [BroadcastReceiver]
    public class AppLocalNotificationsPeriodicUpdateAlarmReceiver : PeriodicUpdateAlarmReceiver<AppLocalNotificationsService, LocalNotificationData>
    {
        public AppLocalNotificationsPeriodicUpdateAlarmReceiver(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public AppLocalNotificationsPeriodicUpdateAlarmReceiver()
        {
        }
    }
}