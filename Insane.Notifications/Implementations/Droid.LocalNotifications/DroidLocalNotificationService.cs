using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content;
using MvvmCross.Plugins.Notifications.Droid.NotificationsBuilder;

namespace Insane.Notifications.Droid.Local
{
    public abstract class DroidLocalNotificationService<TNotificationData> : IntentService where TNotificationData : class
    {
	    private static int notificationId = 0;

        protected DroidLocalNotificationService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected DroidLocalNotificationService(string name) : base(name)
        {
        }

		protected DroidLocalNotificationService() : this(string.Empty)
        {

        }

        protected override async void OnHandleIntent(Intent intent)
        {
            var notificationBuilder = GetNotificationCompatBuilder(); Mvx.Resolve<DroidNotificationCompatBuilder<TNotificationData>>();
            var notificationManager = (NotificationManager)ApplicationContext.GetSystemService(NotificationService);

            foreach (var notificationData in await GetNotificationsData())
            {
                var notificationToShow = notificationBuilder.BuildNotification(ApplicationContext, notificationData);
                notificationManager.Notify(Interlocked.Increment(ref notificationId), notificationToShow);
            }
            
            WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
        }

        protected abstract DroidNotificationCompatBuilder<TNotificationData> GetNotificationCompatBuilder();

        protected abstract Task<IEnumerable<TNotificationData>> GetNotificationsData();
    }
}