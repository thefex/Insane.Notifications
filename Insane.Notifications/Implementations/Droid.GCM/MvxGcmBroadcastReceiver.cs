using Android.Content;
using Insane.Notifications.Droid.GCM.GcmClient;
using MvvmCross.Droid.Platform;

namespace Insane.Notifications.Droid.GCM
{
    public abstract class MvxGcmBroadcastReceiver<TMvxGcmService> : GcmBroadcastReceiverBase<TMvxGcmService>
        where TMvxGcmService : MvxGcmService
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
            setup.EnsureInitialized();

            base.OnReceive(context, intent);


        }

    }
}