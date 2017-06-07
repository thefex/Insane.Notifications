using System;
using Android.Content;
using Insane.Notifications.Droid.Local;
using MvvmCross.Droid.Platform;

namespace Insane.Notifications.Droid.MvxLocal
{
    public abstract class MvxPeriodicUpdateAlarmReceiver<TDroidLocalNotificationService, TNotificationData> :
        PeriodicUpdateAlarmReceiver<TDroidLocalNotificationService, TNotificationData>
        where TDroidLocalNotificationService : DroidLocalNotificationService<TNotificationData>
        where TNotificationData : class
    {
        public MvxPeriodicUpdateAlarmReceiver(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxPeriodicUpdateAlarmReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(this.ApplicationContext);
            setup.EnsureInitialized();

            base.OnReceive(context, intent);
        }
    }
}
