using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Droid.LocalNotifications;
using NotificationsSample.Portable.Data;
using NotificationsSample.Portable.Services;

namespace DroidNotificationsSample.Services.LocalNotifications
{
    // Add [Service] attribute so your AndroidManifest gets updated.
    [Service]
    public class AppLocalNotificationsService : MvxDroidLocalNotificationService<LocalNotificationData>
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