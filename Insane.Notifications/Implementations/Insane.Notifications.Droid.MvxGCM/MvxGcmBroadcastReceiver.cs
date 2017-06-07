using System;
using Android.Content;
using Insane.Notifications.Droid.GCM.GcmClient;
using MvvmCross.Droid.Platform;

namespace Insane.Notifications.Droid.MvxGCM
{
    public abstract class MvxGcmBroadcastReceiver<TMvxGcmService> : GcmBroadcastReceiverBase<TMvxGcmService> where TMvxGcmService : MvxInsaneGcmService
    {
        public MvxGcmBroadcastReceiver(IntPtr javaReference, Android.Runtime.JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public MvxGcmBroadcastReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setup.EnsureInitialized();

            base.OnReceive(context, intent);
        }

    }
}
