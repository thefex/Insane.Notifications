using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content;
using MvvmCross.Droid.Services;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;

namespace Insane.Notifications.Droid.Local
{
    public abstract class MvxDroidLocalNotificationService<TNotificationData> : MvxIntentService where TNotificationData : class
    {
	    private static int notificationId = 0;

        protected MvxDroidLocalNotificationService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected MvxDroidLocalNotificationService(string name) : base(name)
        {
        }

		protected MvxDroidLocalNotificationService() : this(string.Empty)
        {

        }

        protected override async void OnHandleIntent(Intent intent)
        {
            base.OnHandleIntent(intent);

            var notificationBuilder = Mvx.Resolve<MvxDroidNotificationCompatBuilder<TNotificationData>>();
            var notificationManager = (NotificationManager)ApplicationContext.GetSystemService(NotificationService);

            foreach (var notificationData in await GetNotificationsData())
            {
                var notificationToShow = notificationBuilder.BuildNotification(ApplicationContext, notificationData);
                notificationManager.Notify(Interlocked.Increment(ref notificationId), notificationToShow);
            }
            
            WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
        }

        protected abstract Task<IEnumerable<TNotificationData>> GetNotificationsData();
    }
}