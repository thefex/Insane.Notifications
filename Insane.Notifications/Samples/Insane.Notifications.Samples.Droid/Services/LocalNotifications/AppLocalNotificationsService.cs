using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Runtime;
using Insane.Notifications.Droid.Local;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable.Data;
using MvvmCross.Plugins.Notifications.Sample.Portable.Services;

namespace MvvmCross.Plugins.Notifications.Samples.Droid.Services.LocalNotifications
{
    // Add [Service] attribute so your AndroidManifest gets updated.
    [Service]
    public class AppLocalNotificationsService : DroidLocalNotificationService<LocalNotificationData>
    {
        public AppLocalNotificationsService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public AppLocalNotificationsService(string name) : base(name)
        {
        }

        public AppLocalNotificationsService()
        {
        }

        protected override Task<IEnumerable<LocalNotificationData>> GetNotificationsData()
            => Mvx.Resolve<LocalNotificationsProvider>().GetLocalNotifications();
    }
}