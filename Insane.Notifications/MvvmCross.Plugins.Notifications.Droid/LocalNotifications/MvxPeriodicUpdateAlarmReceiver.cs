using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.Plugins.Notifications.Droid.ToRemove;

namespace MvvmCross.Plugins.Notifications.Droid.LocalNotifications
{
    public abstract class MvxPeriodicUpdateAlarmReceiver<TMvxDroidLocalNotificationsService, TNotificationData> :
            MvxWakefulBroadcastReceiver
        where TMvxDroidLocalNotificationsService : MvxDroidLocalNotificationService<TNotificationData>
        where TNotificationData : class
    {
        protected MvxPeriodicUpdateAlarmReceiver(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        protected MvxPeriodicUpdateAlarmReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            base.OnReceive(context, intent);

            var intentService = new Intent(context, typeof(TMvxDroidLocalNotificationsService));
            StartWakefulService(context, intentService);
        }
    }
}