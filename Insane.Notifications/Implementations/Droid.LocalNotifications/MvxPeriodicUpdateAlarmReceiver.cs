using System;
using Android.Content;
using Android.Runtime;
using MvvmCross.Droid.Support.V4;

namespace Insane.Notifications.Droid.Local
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

		protected MvxPeriodicUpdateAlarmReceiver() : base()
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