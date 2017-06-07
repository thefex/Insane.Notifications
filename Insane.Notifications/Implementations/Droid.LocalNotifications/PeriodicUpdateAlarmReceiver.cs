using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.Content;

namespace Insane.Notifications.Droid.Local
{
    public abstract class PeriodicUpdateAlarmReceiver<TDroidLocalNotificationService, TNotificationData> :
            WakefulBroadcastReceiver
        where TDroidLocalNotificationService : DroidLocalNotificationService<TNotificationData>
        where TNotificationData : class
    {
        protected PeriodicUpdateAlarmReceiver(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

		protected PeriodicUpdateAlarmReceiver() : base()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var intentService = new Intent(context, typeof(TDroidLocalNotificationService));
            StartWakefulService(context, intentService);
        }
    }
}